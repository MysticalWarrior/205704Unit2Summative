# 205704Unit2Summative
A program to edit and save contact info from text files.

Problems/notices:

A red box around one of the numbers (like what would appear if you say, entered a leter) means that it is not a valid input, and the program won't save the edit until an accepted one is made.
On the same note; if part of a contact savefile is edited or removed and is no longer a valid input, the value will be set to "null" or 0 in the case of the birthday stuff.

To get AutoSave to work, make sure to run the code through the debugger at least once, and then run the program file in the debug folder.
(The program automatically saves and opens the file from it's program file, but if run through the debugger, it opens the default file rather than the one it automatically creates.)
(Can be fixed also by changing the filepath on MainWindow.cs line 117 to the absolute path of the project's text file.)

Anything could be entered as an email, and it will still work, but it should be noted a valid email is neccessary for a message to be sent.

Warning: any file containing extra data, even if opened by the program, will lose that data when saved to by the program.
ex: if you add a seventh data feild into the text file, it will be lost upon saving.
