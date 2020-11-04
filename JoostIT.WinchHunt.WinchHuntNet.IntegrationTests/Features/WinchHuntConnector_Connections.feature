Feature: WinchuntConnector connections
	In order to know what's happening
	As a software engineer
	I want to have awareness of the connection status to the winchhunt device


Scenario: Winchunt is initially not connected
	Given A new and default WinchuntConnector
	When I do nothing
	Then It should not be connected