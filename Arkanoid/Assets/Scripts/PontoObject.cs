public class PontoObject {
    public string text = "";
    public float x = 0;
    public float y = 0;
    public bool isInc = true;

    public PontoObject(float x, float y, string text, bool isInc){
        this.x = x;
        this.y = y;
        this.text = text;
        this.isInc = isInc;
    }
}