//
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by FernFlower decompiler)
//

public class CMyClass {
    public CMyClass() {
    }

    public native void helloThere();

    public static void main(String[] args) {
        (new CMyClass()).helloThere();
    }

    static {
        System.loadLibrary("MyJavaLib");
    }
}
