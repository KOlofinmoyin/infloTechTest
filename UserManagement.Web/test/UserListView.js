
const { Builder, By, until, Key } = require("selenium-webdriver");

describe("User Management Page", () => {
    let driver;

    beforeEach(async () => {
        driver = await new Builder().forBrowser("chrome").build();
        await driver.manage().setTimeouts({ implicit: 5000 }); // Adjust the timeout if necessary
    });

    afterEach(async () => {
        await driver.quit();
    });

    it("Customer can 'Show All' users in db", async () => {
        const { expect } = await import("chai");

        const baseUrl = "https://localhost:7084/users";
        await driver.get(baseUrl);
        const element = await driver.findElement(By.xpath("//a[contains(text(), 'Show All')]"));
        await driver.executeScript("arguments[0].click();", element);
        // Locate the <tr> element with 'Active Only' attributes using XPath
        const activeElement = await driver.findElement(By.xpath("//tr[td[contains(text(), 'Benjamin Franklin')]]"));

        // Assert that the <tr> element is present
        expect(await activeElement.isDisplayed()).to.be.true;

        // Locate the <tr> element with 'Non Active' attributes using XPath
        const inActiveElement = await driver.findElement(By.xpath("//tr[td[contains(text(), 'Castor')]]"));

        // Assert that the <tr> element is present
        expect(await inActiveElement.isDisplayed()).to.be.true;


    });

    it("Customer can view 'Active Only' users in db", async () => {
        const { expect } = await import("chai");

        const baseUrl = "https://localhost:7084/users";
        await driver.get(baseUrl);
        const element = await driver.findElement(By.xpath("//a[contains(text(), 'Active Only')]"));
        await driver.executeScript("arguments[0].click();", element);

        // Locate the <tr> element with 'Non Active attributes using XPath
        const inActiveElement = await driver.findElement(By.xpath("//tr[td[contains(text(), 'Castor')]]"));

        // Assert that the <tr> element is present
        expect(await inActiveElement.isDisplayed()).to.be.false;


    });
});
