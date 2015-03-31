using System;
using System.Net.Http;
using System.Net.NetworkInformation;

/**
 * Server Class
 * Establish Connection to server
 * Make requests / receive responses
 * \author: Nate Hughes <njh2986@vt.edu>
 */
namespace Hats.ServerInterface
{
public class Server
{
	// the reference to the physical server
	private HttpClient client;

	// server response
	private HttpResponseMessage response;

	/**
 	 * Instantiates a new server object
 	 * \param[in] client Client to connect to and communicate through
 	 */
	public Server(Uri url)
	{			
		client = new HttpClient();
		client.BaseAddress = url;
	}

	/**
 	 * Connect to the remote host
 	 */
	public async void connect()
	{
		try	
		{
			response = await client.GetAsync(client.BaseAddress);
			response.EnsureSuccessStatusCode(); // throws exception if unsuccessful connection
		}
		catch
		{
			// handle exception
		}

	}

	/**
 	 * Get server's response to last request
 	 * \param[out] string representing server's response to latest request
 	 */
	public string getResponse()
	{
		return response.Content.ToString();
	}

	/**
 	 * Ping the server to make sure it's connected and ready to go
 	 */
	public bool serverReady()
	{

		// code reference:
		// http://www.codeproject.com/Tips/109427/How-to-PING-Server-in-C

		string host = string.Format("{0}", client.BaseAddress.Host); 
		Ping p = new Ping();
		try
		{
			PingReply reply = p.Send(host, 3000);
			if (reply.Status == IPStatus.Success)
				return true;
		}
		catch 
		{ 
			// handle exception
		}

		return false;
	}

	/**
 	 * Run the operator's selected test scenario
 	 * \param[in] testScenario string representing scenario chosen by the operator
 	 * \param[out] Config info for the selected test scenario - JSON string
 	 */
	public String runScenario(string testScenario)
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