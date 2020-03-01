Set-Location -Path ..\
Write-Host 'Running gulp command'

docker run --rm -v ${pwd}:/vol node:12-alpine sh -c "npm cache clean -f; cd vol/Frontend; npx gulp;"

