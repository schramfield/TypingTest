#!/usr/bin/env python

import sys
import os
import numpy as np

# main: this is the main function
#
def main(argv):

    # error message/first time user instructions
    #
    if (len(argv) < 2):
        print("BEGIN README\n\tOh, hi! I didn't see you there. \n\tThis is a python program that analyzes log data in .txt format. \n\tIn order to use it, please enter the .txt files you want to analyze as a arguments. \n\tThis program will automatically store the cleaned-up text in a folder called CleanText in your home directory. \n\tGood luck!\nEND README ")
        return(1)

    else:
        print((len(argv)-1), "files to be processed.")
        
    #Now let's cycle through all these data files
    #
    for i in range(1, len(argv)):

        # Open each one
        #
        with open(argv[i]) as CurrentLog:

            # Now open a .txt file with an appropriate name to store your cleaned up typing
            #
            CleanName = CurrentLog.name.strip('DEG:TempDataSorag\e.txt')
            print("opening log", CleanName, "...")
            with open("CleanText/" + CleanName + "_CleanText.txt", 'w') as CleanText:
                
                # read the file into python
                #
                LogRead = CurrentLog.readlines()

                # We need to track some time changes, so here's some variables:
                #
                startTime = [0, 0, 0]
                endTime = [0, 0, 0]
                timeElapsed = 0

                #timerStarted is a switch: 0 means false 1 means true
                #
                timerStarted = 0

                for n, line in enumerate(LogRead):
                                        
                    # use the keyword I planted, "Keystrike:" to identify useful lines
                    #
                    if "Keystrike:" in line:

                        # now split it up into useful tokens
                        #
                        lineTok = line.split(' ')

                        # annoyingly, this needs to be further split up
                        #
                        letterNoLine = lineTok[3].splitlines()

                        # Okay, now we find the non-letter keywords I planted
                        # 'enter' is hit when the participants starts their prompt
                        # so we take the timestamp before and after and calculate time elapsed
                        #
                        if "enter" in letterNoLine[0]:
                            if timerStarted == 0:
                                timerStarted = 1
                                startTime = lineTok[1].split(':') 
                                letter = "\n\n"
                            else:
                                timerStarted = 0
                                endTime = lineTok[1].split(':')
                                hrs = int(endTime[0]) - int(startTime[0])
                                mins = (hrs*60) + (int(endTime[1]) - int(startTime[1]))
                                timeElapsed = (mins*60) + (int(endTime[2]) - int(startTime[2]))
                                letter = "\n" + str(timeElapsed) + " seconds\n"
                        elif "space" in letterNoLine[0]:
                            letter = ' '
                        elif "comma" in letterNoLine[0]:
                            letter = ','
                        elif "period" in letterNoLine[0]:
                            letter = '.'
                            
                        # if the characters aren't special, go ahead and take the letter
                        #
                        else:
                            letter = letterNoLine[0]

                        # write whatver we determined was the correct thing into the clean doc
                        #
                        CleanText.write(letter)

            # I always like a little message to say it worked
            #
            print("Written file ", CleanText.name)

            # always good to close out
            #
            CleanText.close()
        CurrentLog.close()
#
# end of main

# begin gracefully
#
if __name__ == "__main__":
    main(sys.argv)

#
# end of file
