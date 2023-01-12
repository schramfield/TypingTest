using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purpose:
// This is all the prompts I used in my test and the order they were used in hardcoded.
// I don't love the hard coding either and it took a stupidly long time to do
// It worked great though so ..  it's fine.
// This is what you will need to chagne if you want to use different prompts!
//

public class TypingPrompts : MonoBehaviour
{
    public static string[] listPrompts = {
        "Good Job! \n Rest as long as you want, \n then when you are ready to begin the next prompt\n press 'Enter.'",
        "A PERSON WALKS A BLOCK DOWN THE STREET.\nAT THE CORNER THEY STOP,\nAND LOOK BOTH WAYS\nBEFORE CROSSING THE ROAD.",
        "THE DOG DOES NOT LIKE\nTO EAT KIBBLE.\nIT PREFERS TO EAT VEGETABLES,\nSO IT HAD A DINNER OF CARROTS AND PEAS.",
        "MANY TREES GROW IN THE FOREST.\nYOU MUST BE CAREFUL,\nBECAUSE THERE ARE SO MANY TREES\nTHAT IT IS EASY TO GET LOST.",
        "TODAY IS SATURDAY,\nAND THE WEATHER IS BEAUTIFUL.\nMAKE SURE TO TAKE TIME TO\nGO OUTSIDE AND ENJOY THE SUNSHINE.",
        "MATH IS A VERY IMPORTANT SKILL.\nSCHOOL IS A GOOD PLACE TO LEARN MATH,\nBUT YOU CAN ALSO LEARN IT FROM BOOKS.",
        "MOST PLANTS GROW OUT OF THE GROUND.\nHOWEVER, SOME PLANTS GROW FROM THE WATER\nOR OUT OF OTHER PLANTS INSTEAD.",
        "MY PET CAT DOES NOT LIKE MOST PEOPLE.\nSHE LIKES ME VERY MUCH THOUGH,\nAND OFTEN SITS ON MY LAP WHILE I WORK.",
        "BIRDS AND BATS BOTH FLY VERY WELL,\nBUT THEIR WINGS MOVE IN DIFFERENT WAYS.\nYOU CAN TELL THEM APART IN THE AIR.",
        "IT IS DIFFICULT TO WALK IN THE SNOW.\nMAKE SURE YOU HAVE WARM STURDY SHOES,\nOR YOU MIGHT GET COLD FEET.",
        "You have completed part 1. \n Please remove your headset.",
        "You have completed part 2. \n Please remove your headset.",
        "You have completed part 3. \n Please remove your headset.",
        "You have completed part 1.",
        "You have completed part 2.",
        "You have completed part 3.",
        };
    public static int[][][] subjects =
    {   
        new int[3][]{ // SUBJECT 1
            new int[6] { // TRIAL 1 (IRL)
                1, 0, 2, 0, 3, 13
            },
            new int[6]{ // TRIAL 2 (HAPTIC)
                4, 0, 5, 0, 6, 11
            },
            new int[6]{ // TRIAL 3 (VR ONLY)
                7, 0, 8, 0, 9, 12
            }
        },
        new int[3][]{ // SUBJECT 2
            new int[6]{ // TRIAL 1 (HAPTIC)
                4, 0, 5, 0, 7, 10
            },
            new int[6]{ // TRIAL 2 (VR ONLY)
                9, 0, 3, 0, 2, 11
            },
            new int[6]{ // TRIAL 3 (IRL)
                8, 0, 6, 0, 1, 15
            }

        },
        new int[3][]{ // SUBJECT 3
            new int[6]{ // TRIAL 1
                3, 0, 4, 0, 1, 10
            },
            new int[6]{ // TRIAL 2
                5, 0, 8, 0, 2, 14

            },
            new int[6]{ // TRIAL 3
                7, 0, 9, 0, 6, 12
            }
        },
        new int[3][]{ // SUBJECT 4
            new int[6]{ // TRIAL 1
                8, 0, 9, 0, 2, 13
            },
            new int[6]{ // TRIAL 2
                6, 0, 4, 0, 5, 11
            },
            new int[6]{ // TRIAL 3
                7, 0, 3, 0, 1, 12
            }
        },
        new int[3][]{ // SUBJECT 5
            new int[6]{ // TRIAL 1
                6, 0, 1, 0, 7, 10
            },
            new int[6]{ // TRIAL 2
                8, 0, 2, 0, 3, 14
            },
            new int[6]{ // TRIAL 3
                9, 0, 5, 0, 4, 12
            }
        },
        new int[3][]{ // SUBJECT 6
            new int[]{ // TRIAL 1
                1, 0, 6, 0, 9, 10
            },
            new int[6]{ // TRIAL 2
                4, 0, 7, 0, 2, 14
            },
            new int[6]{ // TRIAL 3
                3, 0, 5, 0, 8, 12
            }
        },
        new int[][]{ // SUBJECT 7
            new int[]{ // TRIAL 1
                6, 0, 3, 0, 7, 10
            },
            new int[]{ // TRIAL 2
                5, 0, 9, 0, 1, 14
            },
            new int[]{ // TRIAL 3
                2, 0, 4, 0, 8, 12
            }
        },
        new int[][]{ // SUBJECT 8
            new int[]{ // TRIAL 1
                9, 0, 2, 0, 8, 13
            },
            new int[]{ // TRIAL 2
                6, 0, 7, 0, 3, 11
            },
            new int[]{ // TRIAL 3
                1, 0, 4, 0, 5, 12
            }
        },
        new int[][]{ // SUBJECT 9
            new int[]{ // TRIAL 1
                4, 0, 9, 0, 8, 10
            },
            new int[]{ // TRIAL 2
                7, 0, 6, 0, 5, 11
            },
            new int[]{ // TRIAL 3
                1, 0, 3, 0, 2, 15
            }
        },
        new int[][]{ // SUBJECT 10
            new int[]{ // TRIAL 1
                5, 0, 8, 0, 6, 10
            },
            new int[]{ // TRIAL 2
                2, 0, 7, 0, 9, 11
            },
            new int[]{ // TRIAL 3
                3, 0, 4, 0, 1, 15
            }
        },
        new int[][]{ // SUBJECT 11
            new int[]{ // TRIAL 1
                7, 0, 9, 0, 3, 10
            },
            new int[]{ // TRIAL 2
                4, 0, 8, 0, 5, 11
            },
            new int[]{ // TRIAL 3
                6, 0, 2, 0, 1, 15
            }
        },
        new int[][]{ // SUBJECT 12
            new int[]{ // TRIAL 1
                5, 0, 6, 0, 4, 13
            },
            new int[]{ // TRIAL 2
                7, 0, 9, 0, 1, 11
            },
            new int[]{ // TRIAL 3
                8, 0, 3, 0, 2, 12
            }
        }
    
    };
   
}

// start not required
// update not required
//
// end of script
//
