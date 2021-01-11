# dockerized-twitter-clone
## Laboration in communication between containerized applications.

We have made a simple web-application that enables you yo post messages, and then review all messages in a feed.

We have chosen to use an Angular frontend that consumes an RestAPI (FeedAPI) in order to create messages in a twitter-like application. 
FeedAPI acts as a proxy for the DatabaseAPI which handles the actual persistance.

We decided to use this setup as we reckoned it would be easier to handle the database as a seperate API insead of just a 'pure' database container.

We have not included any unit tests in either of our APIs since there is essentialy no logic that doesn't depend on middleware or external libraries.
 
Our database is mounted to the database container and is also under source control. 

The docker network is created and deleted on docker-compose up/down respectively.

## To run the application: 
###    1: cd to the root folder with the docker-compose file
###    2: in any shell, run `docker-compose up`
