using System;
using System.Net.Http;
//using Hats.StorageInfo

/**
 * Simulated User Creation
 * \author: Nate Hughes <njh2986@vt.edu>
 */

namespace Hats
{

	namespace SimulatedUser
	{

		public class SimulatedUserInterface
		{

			/**
 			* Simulate a user with the provided test scenario
 			* \param[in] info StorageIngo for persistent storage instantiation
 			* \param[in] testScenario testScenario to load for simulation
 			* \param[in] client Client to send the test scenario to
 			* \param[out] Flag indicating success
 			*/
			static public bool createUser(StorageInfo info, string testScenario, HttpClient client)	
			{
			 	return true;
			}

		}
		
	}

}