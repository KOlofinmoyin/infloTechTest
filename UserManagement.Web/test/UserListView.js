
const { Builder, By, until, Key } = require("selenium-webdriver");
const chrome = require('selenium-webdriver/chrome');

describe("User Management Page", () => {
    let driver;

    beforeEach(async () => {
        let options = new chrome.Options();
        options.addArguments('--headless'); // Enable headless mode

        // Add options to clear cache
        options.setUserPreferences({
            'profile.default_content_settings': { 'images': 2 },
            'profile.managed_default_content_settings': { 'images': 2 }
        });


        driver = await new Builder().forBrowser("chrome").setChromeOptions(options).build();
        await driver.manage().setTimeouts({ implicit: 5000 });
        //Maximize current window
        await driver.manage().window().maximize();
    });

    afterEach(async () => {
        await driver.quit();
    });

    it("Should display ALL users when clicking the 'Show All' button", async () => {
        const { expect } = await import("chai");
        const baseUrl = "http://localhost:5000/users";
        await driver.get(baseUrl);
        const element = await driver.findElement(By.xpath("//a[contains(text(), 'Show All')]"));
        await driver.executeScript("arguments[0].click();", element);
        // Locate the <tr> element with 'Active Only' attributes using XPath
        const activeElement = await driver.findElement(By.xpath("//tr[td[contains(text(), 'Benjamin Franklin')]]"));

        // Assert that the Active user is present
        expect(await activeElement.isDisplayed()).to.be.true;

        // Locate the <tr> element with 'Non Active' attributes using XPath
        const inActiveElement = await driver.findElement(By.xpath("//tr[td[contains(text(), 'Castor')]]"));

        // Assert that the 'Non Active' user is present
        expect(await inActiveElement.isDisplayed()).to.be.true;


    });


    it("Should filter out non-active records when clicking 'Active Only' button", async () => {
        const { expect } = await import("chai");

        const baseUrl = "http://localhost:5000/users";
        await driver.get(baseUrl);
        // Click on the 'Active Only' button
        const activeOnlyButton = await driver.findElement(By.xpath("//a[contains(text(), 'Active Only')]"));
        await driver.executeScript("arguments[0].click();", activeOnlyButton);

        // Wait for the page to reload and data to update
        // Locate all elements in the 'Active Account' column
        const activeAccountElements = await driver.findElements(By.xpath("//td[contains(@class, 'active-account')]"));

        // Verify that all elements in the 'Active Account' column have the text 'Yes'
        for (const element of activeAccountElements) {
            const text = await element.getText();
            expect(text).toBe("Yes", "Expected all elements in 'Active Account' column to have text 'Yes'");
        }

        // Ensure that there are no elements with the text 'No' in the 'Active Account' column
        const inactiveAccountElements = await driver.findElements(By.xpath("//td[contains(@class, 'active-account') and contains(text(), 'No')]"));
        expect(inactiveAccountElements.length).to.equal(0, "Expected no elements with text 'No' in 'Active Account' column");
    });


    it("Should filter out active records when clicking 'Non Active' button", async () => {
        const { expect } = await import("chai");

        const baseUrl = "http://localhost:5000/users";
        await driver.get(baseUrl);

        // Click on the 'Non Active' button
        const nonActiveButton = await driver.findElement(By.xpath("//a[contains(text(), 'Non Active')]"));
        // Wait for the page to reload and data to update
        await driver.executeScript("arguments[1].click();", nonActiveButton);

        // Locate all elements in the 'Non Active Account' column
        const nonActiveAccountElements = await driver.findElements(By.xpath("//td[contains(@class, 'non-active-account')]"));

        // Verify that all elements in the 'Non Active Account' column have the text 'No'
        for (const element of nonActiveAccountElements) {
            const text = await element.getText();
            expect(text).toBe("No", "Expected all elements in 'Non Active Account' column to have text 'No'");
        }

        // Ensure that there are no elements with the text 'Yes' in the 'Non Active Account' column
        const activeAccountElements = await driver.findElements(By.xpath("//td[contains(@class, 'non-active-account') and contains(text(), 'Yes')]"));
        expect(activeAccountElements.length).to.equal(0, "Expected no elements with text 'Yes' in 'Non Active Account' column");
    });

});
