public class Pontuacao {
    public int roman = 0;
    public int palisade = 0;
    public int quedas = 0;
    public int clicks = 0;  

    public int total(){
        return romanPts() + palisadePts() + quedasPts() + clicksPts();
    }

    public int palisadePts(){
        return palisade * 1;
    }

    public int romanPts(){
        return roman * 2;
    }

    public int quedasPts(){
        return quedas * -2;
    }

    public int clicksPts(){
        return clicks * -1;
    }

    // T[] InitializeArray<T>(int length) where T : new()
    // {
    //     T[] array = new T[length];
    //     for (int i = 0; i < length; ++i)
    //     {
    //         array[i] = new T();
    //     }

    //     return array;
    // }
}