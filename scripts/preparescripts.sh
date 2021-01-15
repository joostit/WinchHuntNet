#!/bin/bash

echo Making utility scripts executable
parent_path=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

chmod +x $parent_path/install.sh
chmod +x $parent_path/start.sh
chmod +x $parent_path/stop.sh
chmod +x $parent_path/restart.sh
chmod +x $parent_path/showlog.sh
chmod +x $parent_path/update.sh
