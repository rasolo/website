# Setup local environment
PowerShell 4 (to check version, run: `$PSVersionTable.PSVersion` in a PowerShell prompt. [How to upgrade](http://social.technet.microsoft.com/wiki/contents/articles/21016.how-to-install-windows-powershell-4-0.aspx)

**1. Install IIS website, app pools and hosts entries**

- Creates an App Pool named `rasolo.local`
- Creates a website named `rasolo.local` on port 80
- Adds hosts entry for `127.0.0.1 rasolo.local`
- Configures authentication for IIS website

**2. Recycle App Pool** 
- Recycles the App Pool.
**3. Exit**

- Exit this PowerShell script.
