# AESwitcher

Change default audio playback device with one action. Cycles through enabled devices.

### Installation

- Download the latest zip from [releases](https://github.com/unagi-dev/audio-endpoint-switcher/releases)
- Extract to preferred location.
- Run AESwitcher to change playback device.

![alt text](https://raw.githubusercontent.com/unagi-dev/audio-endpoint-switcher/master/AESwitcher/AESwitcher.gif "AESwitcher")


### AudioEndpointController

AudioEndpointController is a fork from https://github.com/DanStevens/AudioEndPointController

Changes
- Added `-c` flag to cycle through active audio outputs.
- Added WFP UI wrapper for cycle notifications. 

A Windows command-line program for listing audio end-points and setting the default

	>EndPointController.exe --help
	Lists active audio end-point playback devices or sets default audio end-point
	playback device.

	USAGE
	  EndPointController.exe [-a] [-f format_str]  Lists audio end-point playback
												   devices that are enabled.
	  EndPointController.exe device_index          Sets the default playback device
												   with the given index.
	  EndPointController.exe -c                    Cycles through active playback devices

	OPTIONS
	  -a             Display all devices, rather than just active devices.
	  -f format_str  Outputs the details of each device using the given format
					 string. If this parameter is ommitted the format string
					 defaults to: "Audio Device %d: %ws"

					 Parameters that are passed to the 'printf' function are
					 ordered as follows:
					   - Device index (int)
					   - Device friendly name (wstring)
					   - Device state (int)
					   - Device default? (1 for true 0 for false as int)
					   - Device description (wstring)
					   - Device interface friendly name (wstring)
					   - Device ID (wstring)