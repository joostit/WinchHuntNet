#!/bin/bash
echo ""
echo ""
echo "WinchHunt REST service installer."
echo "(c) 2020-2021 Joost Haverkort"
echo ""
echo "" 

parent_path=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

echo "   Stopping existing WinchHunt REST Connector service..."
systemctl stop whrestconn.service
sleep 3

echo "Publishing files to /srv/winchuntnet..."
mkdir -p /srv/winchuntnet
dotnet publish $parent_path/../JoostIT.WinchHunt.WhRestConnector/JoostIT.WinchHunt.WhRestConnector.csproj -c Release -o /srv/winchhuntnet/
touch /etc/winchhuntnet.conf

echo "   Creating systemd service unit..."
cp -rf $parent_path/templates/whrestconn.service  /lib/systemd/system/whrestconn.service
chmod 644 /lib/systemd/system/whrestconn.service
systemctl daemon-reload
sudo systemctl enable whrestconn.service

echo ""
echo "Done installing WinchHunt REST Connector."
echo ""
echo "//ToDo: Add or update configuration file: /etc/winchuntnet.conf"
echo "To start WinchHunt REST Connector execute 'systemctl start whrestconn.service'"
echo "or reboot the system"
echo ""
echo ""
