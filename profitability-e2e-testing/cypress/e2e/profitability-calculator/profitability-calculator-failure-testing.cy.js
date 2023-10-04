/// <reference types="cypress" />

describe("API Testing - Profitability Calculator - Failure testing", () => {
    let authToken;

  it("User sign up with empty data", () => {
    cy.request({
      method: "POST",
      url: "/user/signup",
      failOnStatusCode: false,
      body: {},
    }).then((response) => {
      expect(response.status).to.equal(400);
    });
  });

  it("User sign up with weak username", () => {
    cy.fixture("testSignUpWeakUsername").then((data) => {
      cy.request({
        method: "POST",
        url: "/user/signup",
        failOnStatusCode: false,
        body: data,
      }).then((response) => {
        expect(response.status).to.equal(400);
      });
    });
  });

  it("User sign up with weak password", () => {
    cy.fixture("testSignUpWeakPassword").then((data) => {
      cy.request({
        method: "POST",
        url: "/user/signup",
        failOnStatusCode: false,
        body: data,
      }).then((response) => {
        expect(response.status).to.equal(400);
      });
    });
  });

  it("User sign up valid data", () => {
    cy.fixture("testUserSignUpData").then((userData) => {
      cy.request({ method: "POST", url: "/user/signup", body: userData }).then(
        (response) => {
          expect(response.status).to.equal(201);
        }
      );
    });
  });

  it("User login invalid credentials", () => {
    cy.fixture("testLoginInvalidCredentials").then((data) => {
      cy.request({
        method: "POST",
        url: "/user/login",
        failOnStatusCode: false,
        body: data,
      }).then((response) => {
        expect(response.status).to.equal(400);
      });
    });
  });

  it("User login valid credentials", () => {
    cy.fixture("testUserLoginData").then((data) => {
      cy.request({
        method: "POST",
        url: "/user/login",
        failOnStatusCode: false,
        body: data,
      }).then((response) => {
        expect(response.status).to.equal(200);
        expect(response.body).to.have.property("username", "testuser");
        expect(response.body).to.have.property("token").to.be.a("string");
        authToken = response.body.token;
      });
    });
  });

  it("Profitability Calculation - Testing with empty data", () => {
    cy.fixture("emptyData").then(data => {
      cy.request({
        method: "POST",
        url: "/profitabilityCalculation",
        headers: {
          Authorization: `Bearer ${authToken}`,
        },
        body: data,
      }).then((response) => {
        expect(response.status).to.equal(200);
        expect(response.body).to.have.property("totalDistanceBasedCost", 0);
        expect(response.body).to.have.property("totalTimeBasedCost", 0);
        expect(response.body).to.have.property("income", 0);
        expect(response.body).to.have.property("profitability", 0);
      });
    });
  });

  it("Profitability Calculation - Testing with invalid data", () => {
    cy.fixture("testProfitabilityCalculationInvalidData").then(data => {
      cy.request({
        method: "POST",
        url: "/profitabilityCalculation",
        headers: {
          Authorization: `Bearer ${authToken}`,
        },
        failOnStatusCode: false,
        body: data,
      }).then((response) => {
        expect(response.status).to.equal(400);
      });
    });
  });

  it("Profitability Calculation - Testing with valid data", () => {
    cy.fixture("testProfitabilityCalculationData").then(data => {
      cy.request({
        method: "POST",
        url: "/profitabilityCalculation",
        headers: {
          Authorization: `Bearer ${authToken}`,
        },
        body: data,
      }).then((response) => {
        expect(response.status).to.equal(200);
        expect(response.body).to.have.property("totalDistanceBasedCost", 50);
        expect(response.body).to.have.property("totalTimeBasedCost", 40);
        expect(response.body).to.have.property("income", 100);
        expect(response.body).to.have.property("profitability", 10);
      });
    });
  });

  it("Test data removal validation", () => {
    cy.fixture("testUserDeleteData").then(userData => {
      cy.request({
        method: "DELETE",
        url: "/user/delete",
        headers: {
          Authorization: `Bearer ${authToken}`,
        },
        body: userData,
      }).then((response) => {
        expect(response.status).to.equal(200);
      });
    })
  });
});
