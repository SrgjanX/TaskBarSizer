# Windows 11 task bar sizer script
Pretty straightforward batch script for quick taskbar resize in Windows 11.<br/>
When ran it will ask for a number to be entered between 1-3 (1:Small, 2:Normal, 3:Big)

## How It Works
- Waits for number input.
- Sets the corresponding registry with a value between 0-2 (input - 1)
- Refreshes the Explorer.exe by stopping it and starting it again.