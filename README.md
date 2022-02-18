# Vigilate
Tiny service to prevent a PC from locking.
This runs happily in the background and uses a system call to prevent the PC locking, there are no keys pressed or visible interference to the user and resource usage will be tiny.

## Build and Install
Early build can be used fine, however there is no official installer **yet**
Download the source, run install.bat as administrator which will handle building the code, installing locally and adding the service. 

## Uninstall 
To remove, you can manually remove the folder it is installed to (by default C:\Program Files\Vigilate but you can modify the batch file)
Then run sc.exe delete Vigilate