// #docregion
module.exports = function (config) {

    var appBase     = 'dist/app/'; // transpiled app JS and map files
    var appSrcBase  = 'src/app/';  // app source TS files
    var appAssets   = 'base/app/'; // component assets fetched by Angular's compiler

    var testBase    = 'testing/';  // transpiled test JS and map files
    var testSrcBase = 'testing/';  // test source TS files

    config.set({
        basePath: '',
        frameworks: ['jasmine'],
        plugins: [
            require('karma-jasmine'),
            require('karma-chrome-launcher'),
            require('karma-jasmine-html-reporter'), // click "Debug" in browser to see it
            require('karma-htmlfile-reporter')      // crashing w/ strange socket error
        ],
        files: [
            'dist/external/systemjs/dist/system.src.js',

            'dist/external/core-js/client/shim.js',
            'dist/external/reflect-metadata/Reflect.js',

            'dist/external/zone.js/dist/zone.js',
            'dist/external/zone.js/dist/long-stack-trace-zone.js',
            'dist/external/zone.js/dist/proxy.js',
            'dist/external/zone.js/dist/sync-test.js',
            'dist/external/zone.js/dist/jasmine-patch.js',
            'dist/external/zone.js/dist/async-test.js',
            'dist/external/zone.js/dist/fake-async-test.js',

            { pattern: 'dist/external/rxjs/**/*.js', included: false, watched: false },
            { pattern: 'dist/external/rxjs/**/*.js.map', included: false, watched: false },

            { pattern: 'dist/external/@angular/**/*.js', included: false, watched: false },
            { pattern: 'dist/external/@angular/**/*.js.map', included: false, watched: false },

            { pattern: 'dist/external/@ng-bootstrap/ng-bootstrap/**/*', included: false, watched: false },

            { pattern: appBase + 'systemjs.config.js', included: false, watched: false },
            { pattern: appBase + 'systemjs.config.extras.js', included: false, watched: false },

            'karma-test-shim.js',

            { pattern: appBase + '**/*.js', included: false, watched: true },
          
            { pattern: appBase + '**/*.html', included: false, watched: true },
            { pattern: appBase + '**/*.css', included: false, watched: true },
        ],
        proxies: {
            "/app/": appAssets,
            "/external/": 'node_modules/'
        },
        exclude: [],
        preprocessors: {},
        reporters: ['progress', 'kjhtml'],
        port: 9876,
        colors: true,
        logLevel: config.LOG_INFO,
        autoWatch: true,
        browsers: ['Chrome'],
        singleRun: false
    })
}
