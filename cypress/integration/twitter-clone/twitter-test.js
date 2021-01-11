context('twitter-clone tests', ()=>{
    before(()=>{
        cy.intercept('GET','http://localhost:8080/message').as('getMessages')
        cy.intercept('POST','http://localhost:8080/message').as('postMessage')
        cy.visit('http://localhost:4200');
        cy.wait(['@getMessages'])
    })
    after(()=>{
        cy.request(
            'DELETE',
            'http://localhost:8080/message/',
            {"content": "TestMessageSecret1337" }
        ) 
    })
    it('post message', () => {
        //Fill out form and submit
        cy.get('#contentArea')
            .type('TestMessageSecret1337')
        cy.get('#authorField')
            .type('AutoAuthor')
        cy.get('#postButton')
            .click()
        
        //Wait for the POST request, and refreshing GET request
        cy.wait(['@postMessage', '@getMessages'])

        //Look through all messages for our newly created message
        cy.get('.tweet > #author')
            .contains('AutoAuthor')        
        cy.get('.tweet > #content')
            .contains('TestMessageSecret1337')

        //Make sure the form is reset
        cy.get('#contentArea')
            .should('have.value','')
        cy.get('#authorField')
            .should('have.value','')
    });
});