/// <reference types="cypress" />

describe("API Testing - Profitability Calculation", () => {
  const testUserUsername = "testuser";
  let authToken;

  it("User sign up validation", () => {
    cy.fixture("testUserSignUpData").then((userData) => {
      cy.request({ method: "POST", url: "/user/signup", body: userData }).then(
        (response) => {
          expect(response.status).to.equal(201);
        }
      );
    });
  });

  it("User login validation", () => {
    cy.fixture("testUserLoginData").then(userData => {
      cy.request({
        method: "POST",
        url: "/user/login",
        body: userData,
      }).then((response) => {
        expect(response.status).to.equal(200);
        expect(response.body).to.have.property("username", testUserUsername);
        expect(response.body).to.have.property("token").to.be.a("string");
        authToken = response.body.token;
      });
    })
  });

  it("Profitability Calculation - Without JWT token bearer", () => {
    cy.fixture("testProfitabilityCalculationData").then(data => {
      cy.request({
        method: "POST",
        url: "/profitabilityCalculation",
        failOnStatusCode: false,
        body: data,
      }).then((response) => {
        expect(response.status).to.equal(401);
      });
    });
  });

  it("Profitability Calculation - With JWT token bearer", () => {
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

  it("Test user removal validation", () => {
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
