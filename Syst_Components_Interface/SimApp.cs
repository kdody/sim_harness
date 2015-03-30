using System;
using System.Net.Http;


/**
 * SimApp Class
 * Instantiate user apps
 * \author: Nate Hughes <njh2986@vt.edu>
 */

namespace Hats
{
	namespace SimAppInterface
	{

		public class SimApp
		{

			/**
 			* Instantiates a SimApp object - a representation of an end-user's app
 			* \param[in] scenarioConfig String containing the scenario config in JSON format
 			*/
			public SimApp(String scenarioConfig)
			{
				// execute an app process with the given scenario configuration	
			}

			// NOTE: the ready signal is gotten via the server, not directly from the app

		}
	}
}