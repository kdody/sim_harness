using System;
using System.Net.Http;


/**
 * SimHouse Class
 * Instantiate simulated houses
 * \author: Nate Hughes <njh2986@vt.edu>
 */

namespace Hats
{
	namespace SimHouseInterface
	{
		public class SimHouse
		{
			
			/**
 			* Instantiates a SimHouse object - a simulated house
 			* \param[in] scenarioConfig String containing the scenario config in JSON format
 			*/
			public SimHouse(String scenarioConfig)
			{
				// execute a simulated house process with the given scenario configuration	
			}

			// NOTE: the ready signal is gotten via the server, not directly from the sim house
		}
	}
}