Write-Output "Running npm install from docker"
docker run -v ${pwd}:/volume node:12-alpine /bin/bash -c "cd volume && npm install"