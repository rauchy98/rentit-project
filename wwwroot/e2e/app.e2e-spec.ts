import { RentItPage } from './app.po';

describe('rent-it App', () => {
  let page: RentItPage;

  beforeEach(() => {
    page = new RentItPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
