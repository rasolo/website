Write-Output "Running npm install from docker"
docker run -v ${pwd}:/volume node:10 /bin/bash -c "cd volume && npm install"