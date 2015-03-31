using System;
using System.Net.Http;
//using Hats.Scenario


/**
 * Server Class
 * Establish Connection to server
 * Make requests / receive responses
 * \author: Nate Hughes <njh2986@vt.edu>
 */

namespace Hats
{
	namespace ServerInterface
	{
		public class Server
		{
			private HttpClient client; // the reference to the physical server

			/**
 			* Instantiates a new server object
 			* \param[in] client Client to connect to and communicate through
 			*/
			public Server(HttpClient client)
			{
				this.client = client;

				// connect to the client
			}

			/**
 			* Ping the server to make sure it's connected and ready to go
 			*/
			public bool serverReady()
			{
				return true;
			}

			/**
 			* Run the operator's selected test scenario
 			* \param[in] testScenario ***Scenario chosen by the operator
 			* \param[out] Config info for the selected test scenario - JSON string
 			* 
 			* *** class needs to be defined
 			* 
 			*/
			public String runScenario(/*Scenario testScenario*/)
			{
				return "";
			}

			/**
 			* Free any simulation-dedicated resources
 			*/
			public bool cleanUp()
			{
				return true;
			}

			/**
 			* Query the server for the state of the spawned apps and houses 
 			*/
			public bool simReady()
			{
				return true;
			}

			/**
 			* Send a go signal to the server
 			* Precondition: Ensure all component instances are ready - call simReady()
 			*/
			public bool startSim()
			{
				return true;
			}

		}
	}
}