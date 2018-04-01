import * as phantom from 'phantom';

export class Browser {
  private browser: phantom.PhantomJS;
  private disposed: boolean;
  private later: Promise<void>;

  constructor() {
    this.later = this.start();
    this.disposed = false;
  }
  
  public async openUrl(url: string): Promise<phantom.WebPage> {
    return this.later.then(async () => {
      const page = await this.browser.createPage();
      await page.open(url);

      return page;
    });
  }
  public dispose(): void {
    if (this.disposed) {
      return;
    }

    if (this.browser) {
      this.browser.exit();
    }

    this.disposed = true
  }

  private async start(): Promise<void> {
    this.browser = await phantom.create();
  }
}