/// <binding ProjectOpened='default' />
'use strict';

const gulp       = require('gulp');
const gutil      = require('gulp-util');
const debug      = require('gulp-debug');
const pug        = require('gulp-pug');
const sass       = require('gulp-sass');
const ts         = require('gulp-typescript');
const tslint     = require('gulp-tslint');
const del        = require('del');
const args       = require('yargs').argv;
const childproc  = require('child_process');
const runSeq     = require('run-sequence');
const server     = require('live-server');
const sourcemaps = require('gulp-sourcemaps');
const merge      = require('merge-stream');
const path       = require('path');

let initialized = false;
let pugOptions = { pretty: true, doctype: 'html' };
let mincss = gutil.noop;
let minjs = gutil.noop;

const dest = {
    root: 'dist',
    external: {
        root: 'dist/external',
        css: 'dist/external/css',
        fonts: 'dist/external/fonts',
    },
    app: 'dist/app',
    customCss: 'dist/css',
}
const src = {
    app: 'src/app',
    deletables: [
      'dist/**/*'
    ],
    external: {
        base: 'node_modules',
        static: ['src/favicon.ico'],
        fonts: ['node_modules/bootstrap/dist/fonts/**/*'],
        js: [
          'node_modules/@angular/**/*',
          'node_modules/@ng-bootstrap/**/*',
          'node_modules/zone.js/**/*',
          'node_modules/reflect-metadata/**/*',
          'node_modules/systemjs/**/*',
          'node_modules/rxjs/**/*',
          'node_modules/bootstrap/**/*',
          'node_modules/jquery/**/*',
          'node_modules/core-js/**/*',
          '!node_modules/rxjs/src/**/*'
        ],
        css: [
          'node_modules/bootstrap/**/*',
          'src/css/themes/*.css'
        ]
    },
    styles: {
        custom: 'src/css/*.css',
        components: ['src/app/**/*.sass'],
    },
    pugs: {
        index: 'src/index.pug',
        components: ['src/app/**/*.pug']
    },
    ts: {
        root: ['src/main.ts', 'src/system.config.ts'],
        app: ['src/app/**/*.ts']
    },
};

gulp.task('config', () => {
    config();
});

gulp.task('clean', ['config'], () => {
    if (args.nodelete) {
        return;
    }

    return del(src.deletables);
});

gulp.task('default', ['config', 'clean'], () => {
    const done = merge(buildExternal(), buildHtml(), buildCss(), buildJs());

    if (!args.w) {
        watch();
    }

    return done;
});

gulp.task('build-html', ['config'], () => {
    return buildHtml();
});

gulp.task('build-css', ['config'], () => {
    return buildCss();
});

gulp.task('build-js', ['config'], () => {
    return buildJs();
});

gulp.task('serve', () => {
    const config = {
        port: 8082,
        root: './dist',
        wait: 1000,
        file: 'index.html'
    };

    server.start(config);
});

function config() {
    if (!args.production) {
        return;
    }

    log(`configuring for production`);

    pugOptions.pretty = false;
    mincss = require('gulp-clean-css');
    minjs = require('gulp-uglify');

    initialized = true;
}

function buildExternal() {
    const staticdeps = gulp.src(src.external.static)
      .pipe(gulp.dest(dest.root));

    const fonts = gulp.src(src.external.fonts)
      .pipe(gulp.dest(dest.external.fonts));

    const jsdeps = gulp.src(src.external.js, { base: src.external.base })
      .pipe(gulp.dest(dest.external.root));

    const cssdeps = gulp.src(src.external.css)
      .pipe(gulp.dest(dest.external.css));

    return merge(staticdeps, fonts, jsdeps, cssdeps);
}

function buildHtml() {
    const buildIndex = configureHtmlBuild(src.pugs.index, dest.root);
    const buildViews = configureHtmlBuild(src.pugs.components, dest.app, src.app);

    return merge(buildIndex, buildViews);
}

function buildCss() {
    const buildComponents = configureCssBuild(src.styles.components, dest.app)
    const buildCustom = configureCssBuild(src.styles.custom, dest.customCss);

    return merge(buildCustom, buildComponents);
}

function buildJs() {
    const buildRoot = configureTsBuild(src.ts.root, dest.root);
    const buildApp = configureTsBuild(src.ts.app, dest.app, src.app);

    return merge(buildRoot, buildApp);
}

function watch() {
    log("watching for changes");
    watchHtml();
    watchCss();
    watchJs();
}

function configureHtmlBuild(src, dest, base) {
    return gulp.src(src, { base: base })
      .pipe(pug(pugOptions))
      .pipe(gulp.dest(dest))
      .pipe(debug());
}

function configureCssBuild(src, dest, base) {
    let config = gulp.src(src, { base: base });

    return config
        .pipe(sourcemaps.init())
        .pipe(sass({ outputStyle: 'expanded' }).on('error', sass.logError))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(dest))
        .pipe(debug());
}

gulp.task('debug-js', ['config'], () => {
    let src = 'D:\\Projects\\github\\tmog\\client\\src\\app\\tmog-sets\\tmog-set-add\\tmog-set-add.component.spec.ts'


    let dest = 'dist/app';
    let base = 'src/app';
    const path = toRelativePath(src);
    console.log(path);
    configureTsBuild(src, dest, base);
});

function configureTsBuild(src, dest, base) {
    const tsProject = ts.createProject('tsconfig.json');
    return gulp.src(src, { base: base })
        .pipe(tslint({ formatter: 'verbose' }))
        .pipe(tslint.report({ emitError: false }))
        .pipe(sourcemaps.init())
        .pipe(ts({
            emitDecoratorMetadata: true,
            experimentalDecorators: true,
            target: 'es5'
        }))
        .js
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(dest))
        .pipe(debug())
}

function watchHtml() {
    gulp.watch(src.pugs.index, (event) => {
        header('building html index file');
        return configureHtmlBuild(src.pugs.index, dest.root);
    });

    gulp.watch(src.pugs.components, (event) => {
        header('building html component');

        const handled = handleFileDeletion(event, src.app, dest.app, ['html']);

        if (handled) {
            return;
        }

        return configureHtmlBuild(event.path, dest.app, src.app);
    });
}

function watchCss() {
    gulp.watch(src.styles.custom, event => {
        header('building css custom file');
        return configureCssBuild(src.styles.custom, dest.customCss);
    });

    gulp.watch(src.styles.components, (event) => {
        header('building css components');

        const handled = handleFileDeletion(event, src.app, dest.app, ['css', 'css.map']);

        if (handled) {
            return;
        }

        return configureCssBuild(event.path, dest.app, src.app);
    });
}

function watchJs() {
    gulp.watch(src.ts.root, (event) => {
        header('building js index file');
        return configureTsBuild(src.ts.root, dest.root);
    });

    gulp.watch(src.ts.app, (event) => {
        header('building js files');

        const handled = handleFileDeletion(event, src.app, dest.app, ['js']);

        if (handled) {
            return;
        }

        //const path = toRelativePath(event.path);
        //return configureTsBuild(path, dest.app, src.app);
        return buildJs();
    });
}

function handleFileDeletion(event, srcBasePath, destBasePath, fileExtensions) {
    if (event.type != 'deleted') {
        return false;
    }

    log(`Source file removed: ${event.path}`);

    let deletedFile = event.path.replace(/\\/g, '/');
    deletedFile = deletedFile.replace(path.extname(event.path), ''); // remove file extension
    deletedFile = deletedFile.replace(srcBasePath, destBasePath);

    let deletables = [];
    for (let i = 0; i < fileExtensions.length; i++) {
        deletables.push(`${deletedFile}.${fileExtensions[i]}`);
    }

    del(deletables).then((files) => {
        if (files) {
            files.forEach(file => {
                log(`Removed corresponding file: ${file}`);
            });
        }
    });

    return true;
}

function log(message) {
    gutil.log(gutil.colors.yellow(message));
}

function header(message) {
    gutil.log(gutil.colors.green(`------------------ ${message} ------------------`));
}

function error(error) {
    gutil.log(gutil.colors.red(`error in less file: ${error.message}`));
}

function toRelativePath(path) {
    path = path.replace(/\\/g, '/');
    path = path.substr(path.indexOf(src.app));
    return path;
}
