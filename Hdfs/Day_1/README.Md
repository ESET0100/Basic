PC@DESKTOP-49AG27E MINGW64 ~ (master)
$ ls
 AppData/
'Application Data'@
 Contacts/
 Cookies@
 Desktop/
 Documents/
 Downloads/
 Favorites/
 Links/
'Local Settings'@
 Music/
'My Documents'@
 NTUSER.DAT
 NTUSER.DAT{737a4f49-8401-11f0-bf13-95929f7d7378}.TM.blf
 NTUSER.DAT{737a4f49-8401-11f0-bf13-95929f7d7378}.TMContainer00000000000000000001.regtrans-ms
 NTUSER.DAT{737a4f49-8401-11f0-bf13-95929f7d7378}.TMContainer00000000000000000002.regtrans-ms
 NetHood@
 OneDrive/
 Pictures/
 Postman/
 PrintHood@
 Recent@
'Saved Games'/
 Searches/
 SendTo@
'Start Menu'@
 Templates@
 Videos/
 node_modules/
 ntuser.dat.LOG1
 ntuser.dat.LOG2
 ntuser.ini
 package-lock.json
 package.json
 source/

PC@DESKTOP-49AG27E MINGW64 ~ (master)
$ cd Python
bash: cd: Python: No such file or directory

PC@DESKTOP-49AG27E MINGW64 ~ (master)
$ cd Desktop

PC@DESKTOP-49AG27E MINGW64 ~/Desktop (master)
$ cd Python

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ touch hi.sh

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ mkdir New

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ echo "Hello"
Hello

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ name="Lakshay"

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ echo "name"
name

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ ech0 $(name)
bash: name: command not found
bash: ech0: command not found

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ ech0 $name
bash: ech0: command not found

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ echo $name
Lakshay

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ read age
22

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ echo $age
22

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ echo $HOME
/c/Users/PC

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ echo $SHELL
/usr/bin/bash

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ echo $USER


PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ echo $PATH
/c/Users/PC/bin:/mingw64/bin:/usr/local/bin:/usr/bin:/bin:/mingw64/bin:/usr/bin:/c/Users/PC/bin:/c/WINDOWS/system32:/c/WINDOWS:/c/WINDOWS/System32/Wbem:/c/WINDOWS/System32/WindowsPowerShell/v1.0:/c/WINDOWS/System32/OpenSSH:/cmd:/c/Program Files (x86)/Microsoft SQL Server/160/Tools/Binn:/c/Program Files/Microsoft SQL Server/160/Tools/Binn:/c/Program Files/Microsoft SQL Server/Client SDK/ODBC/170/Tools/Binn:/c/Program Files/Microsoft SQL Server/160/DTS/Binn:/c/Program Files (x86)/Microsoft SQL Server/160/DTS/Binn:/c/Program Files/nodejs:/c/MinGW/bin:/c/Program Files/Microsoft SQL Server/150/Tools/Binn:/c/Program Files/dotnet:/c/Program Files/RabbitMQ Server:/c/Users/PC/AppData/Local/Programs/Python/Python313/Scripts:/c/Users/PC/AppData/Local/Programs/Python/Python313:/c/Users/PC/AppData/Local/Microsoft/WindowsApps:/:/c/Users/PC/AppData/Local/Programs/Microsoft VS Code/bin:/c/Users/PC/AppData/Roaming/npm:/c/Users/PC/.dotnet/tools:/usr/bin/vendor_perl:/usr/bin/core_perl

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ mkdir folder_1 folder_2 folder-3

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ vi hi.sh

[1]+  Stopped                 vi hi.sh

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ vi hi.sh

[2]+  Stopped                 vi hi.sh

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ dir
New  folder-3  folder_1  folder_2  hi.sh  my_env  test.py

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ ls
New/  folder-3/  folder_1/  folder_2/  hi.sh  my_env/  test.py

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ bash hi.sh
hi.sh: line 1: hi: command not found

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ bash hi.sh
hi.sh: line 1: hi: command not found

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ bash test.py
test.py: line 1: syntax error near unexpected token `('
test.py: line 1: `a=int(input('enter the breadth: '))'

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ bash test.py
test.py: line 1: syntax error near unexpected token `('
test.py: line 1: `a=int(input('enter the breadth: '))'

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ bash test.py
test.py: line 1: syntax error near unexpected token `('
test.py: line 1: `a=int(input('enter the breadth: '))'

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ ls -al
total 10
drwxr-xr-x 1 PC 197121   0 Sep 23 14:59 ./
drwxr-xr-x 1 PC 197121   0 Sep 22 15:18 ../
drwxr-xr-x 1 PC 197121   0 Sep 23 14:29 New/
drwxr-xr-x 1 PC 197121   0 Sep 23 14:45 folder-3/
drwxr-xr-x 1 PC 197121   0 Sep 23 14:45 folder_1/
drwxr-xr-x 1 PC 197121   0 Sep 23 14:45 folder_2/
-rw-r--r-- 1 PC 197121  10 Sep 23 15:00 hi.sh
drwxr-xr-x 1 PC 197121   0 Sep  2 17:15 my_env/
-rw-r--r-- 1 PC 197121 106 Sep  3 14:05 test.py

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ chmod u+x
chmod: missing operand after ‘u+x’
Try 'chmod --help' for more information.

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ chmod -- help
chmod: missing operand after ‘help’
Try 'chmod --help' for more information.

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ chmod --help
Usage: chmod [OPTION]... MODE[,MODE]... FILE...
  or:  chmod [OPTION]... OCTAL-MODE FILE...
  or:  chmod [OPTION]... --reference=RFILE FILE...
Change the mode of each FILE to MODE.
With --reference, change the mode of each FILE to that of RFILE.

  -c, --changes          like verbose but report only when a change is made
  -f, --silent, --quiet  suppress most error messages
  -v, --verbose          output a diagnostic for every file processed
      --no-preserve-root  do not treat '/' specially (the default)
      --preserve-root    fail to operate recursively on '/'
      --reference=RFILE  use RFILE's mode instead of MODE values
  -R, --recursive        change files and directories recursively
      --help     display this help and exit
      --version  output version information and exit

Each MODE is of the form '[ugoa]*([-+=]([rwxXst]*|[ugo]))+|[-+=][0-7]+'.

GNU coreutils online help: <https://www.gnu.org/software/coreutils/>
Report any translation bugs to <https://translationproject.org/team/>
Full documentation <https://www.gnu.org/software/coreutils/chmod>
or available locally via: info '(coreutils) chmod invocation'

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ ls
New/  folder-3/  folder_1/  folder_2/  hi.sh  my_env/  test.py

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ chmod u+x+r test.py

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ bash test.py
test.py: line 1: syntax error near unexpected token `('
test.py: line 1: `a=int(input('enter the breadth: '))'

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ cd ..

PC@DESKTOP-49AG27E MINGW64 ~/Desktop (master)
$ cd ..

PC@DESKTOP-49AG27E MINGW64 ~ (master)
$ cd Home
bash: cd: Home: No such file or directory

PC@DESKTOP-49AG27E MINGW64 ~ (master)
$ ls
 AppData/             NTUSER.DAT                                                                                     SendTo@
'Application Data'@   NTUSER.DAT{737a4f49-8401-11f0-bf13-95929f7d7378}.TM.blf                                       'Start Menu'@
 Contacts/            NTUSER.DAT{737a4f49-8401-11f0-bf13-95929f7d7378}.TMContainer00000000000000000001.regtrans-ms   Templates@
 Cookies@             NTUSER.DAT{737a4f49-8401-11f0-bf13-95929f7d7378}.TMContainer00000000000000000002.regtrans-ms   Videos/
 Desktop/             NetHood@                                                                                       node_modules/
 Documents/           OneDrive/                                                                                      ntuser.dat.LOG1
 Downloads/           Pictures/                                                                                      ntuser.dat.LOG2
 Favorites/           Postman/                                                                                       ntuser.ini
 Links/               PrintHood@                                                                                     package-lock.json
'Local Settings'@     Recent@                                                                                        package.json
 Music/              'Saved Games'/                                                                                  source/
'My Documents'@       Searches/

PC@DESKTOP-49AG27E MINGW64 ~ (master)
$ cd Desktop

PC@DESKTOP-49AG27E MINGW64 ~/Desktop (master)
$ cd Python

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ touch test.sh

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ vi test.sh

PC@DESKTOP-49AG27E MINGW64 ~/Desktop/Python (master)
$ bash test.sh
What is your name
lakshay
what is your age?
22
Your name is lakshay and age is 22

