// Last modifided: March 22nd, 2015
// Modified by: Clayton Kuchta

using System;
using System.Net.Http;

namespace Hats
{
	namespace device_commands
	{
		static public class DeviceCommands
		{
			/**
            * Given a device information sends a create simmulated device to a server
            * \param[in] devClass: This is the type of class of the device
            * \param[in] devType: This is a subset of classes and it's the type of device for a particular class
            * \param[in] devName: The name we want to give the device
            * \param[in] houseID: The house ID we want to add the device to
            * \param[in] roomID: The room we want to add the device to
            * \param[in] client Client to send the device to
            * \param[out] integer indicating what the device ID is
            */
			static public int createDevice (string devClass, string devType, string devName, int houseID, int roomID, HttpClient client)
			{
				return 0;
			}

			/**
            * Given a house, room and device ID we can locate the device and delete it
            * \param[in] houseID: The house ID the device lies in
            * \param[in] roomID: The room the device lies in
            * \param[in] devID: The device we want to delete
            * \param[in] client: Client to send the device to delete
            * \param[out] Flag indicating if the device was found and deleted
            */
			static public bool removeDevice (int houseID, int roomID, int devID, HttpClient client)
			{
				return false;
			}

			/**
            * Given a house, room, device, and a new name it will update the name of a device showing up for a user
            * \param[in] houseID: The house ID the device lies in
            * \param[in] roomID: The room the device lies in to change the name
            * \param[in] devID: The device ID that needs to have its name changed
            * \param[in] newName: String containing the new name for the device
            * \param[in] client: Client to send the information to
            * \param[out] Flag indicating if the device name was found and deleted
            */            
			static public bool changeDeviceName(int houseID, int roomID, int devID, string newName, HttpClient client)
			{
				return false;
			}
		}
	}
}
