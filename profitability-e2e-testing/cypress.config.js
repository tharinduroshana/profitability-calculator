const { defineConfig } = require("cypress");

module.exports = defineConfig({
  e2e: {
    baseUrl: "http://localhost:5105/",
    screenshotOnRunFailure: false,
    video: false
  },
});
