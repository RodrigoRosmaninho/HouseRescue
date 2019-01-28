using System;

public abstract class Levels {
    public static int getIntFromSceneName(String name){
        switch(name) {
            case "Level1":
                return 1;
            case "Level2":
                return 2;
            case "Level3":
                return 3;
            case "Level4":
                return 4;
            default:
                throw new Exception();
        }
    }
}
