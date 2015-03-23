using System;

/**
 * Class for simulating users
 * \author: Nate Hughes <njh2986@vt.edu>
 */

namespace Hats
{

	namespace User
	{

		public class User
		{

			/** A constructor.
			* @param username is the value that will set the internal username
			* @param password is the value that will set the internal password
			**/
			User(string username, string password){
				updateUsername(username);
				updatePassword(password);
			}

			User(){
				updateUsername(string.Empty);
				updatePassword(string.Empty);
			}

			/**A function that takes no arguments and returns the username and password.
			@return The username, password, and other user information
			*/
			string[] getInfo(){
				return new string[5 /*number of settings plus 2*/]; //return username and password appended to the user info array
			}

			/**A function that updates the internal login username
			* @param username is the value that will set the internal username
			**/
			void updateUsername(string username){

			}

			/**A function that updates the internal login password
			* @param password is the value that will set the internal password
			**/
			void updatePassword(string password){

			}

			/**A function that updates the internal user settings
			* @param settings is the array that will be the user's settings
			**/
			void updateSettings(string[] settings){

			}

			/**A function that returns the username
		    * @return username
		    * */
			string getUsername(){
				return "";
			}

			/**A function that returns the user's password
		 	* @return password
		 	* */
			string getPassword(){
				return "";
			}

			/**A function that returns the user's settings
		 	* @return settings
		 	* */
			string[] getSettings(){
				return new String[5];
			}

			/**A function that creates a new user in the database.
		 	* */
			bool createUser(){
				return true;
			}

			/**A function that updates the user's information
		 	* on the database.
		 	* */
			bool updateUser(){
				return true;
			}

		}

	}

}
