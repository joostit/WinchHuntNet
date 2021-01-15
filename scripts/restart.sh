#!/bin/bash
echo Stopping WinchHunt REST Connector service
systemctl stop whrestconn.service
sleep 3
systemctl start whrestconn.service
