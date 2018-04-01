declare var window: any;

import * as phantom from 'phantom';

export abstract class BaseDataProvider<T> {
  private browser: phantom.PhantomJS;

  constructor(private dataPage: string) {

  }

  public async getData(): Promise<T> {
    await this.start();

    let retVal: T;
    const page = await this.getPage();

    retVal = await this.getDataFromPage(page);

    this.stop();

    return retVal;
  }

  protected abstract getDataFromPage(page: phantom.WebPage): Promise<T>;

  private async start(): Promise<void> {
    this.browser = await phantom.create();
  }

  private async getPage(): Promise<phantom.WebPage> {
    const page = await this.browser.createPage();
    const status = await page.open(this.dataPage);

    return page;
  }

  private stop(): void {
    if (this.browser) {
      this.browser.exit();
    }
  }
}