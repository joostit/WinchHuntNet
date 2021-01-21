#!/bin/bash
sudo journalctl -e -f -u whrestconn.service -o cat --lines=100

