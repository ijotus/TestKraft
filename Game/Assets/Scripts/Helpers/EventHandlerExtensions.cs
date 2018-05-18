using System;

public static class EventHandlerExtensions
{
    public static void SafeRaise(this EventHandler handler, object sender, EventArgs e)
    {
        if (handler != null)
            handler(sender, e);
    }
    public static void SafeRaise<TEventArgs>(this EventHandler<TEventArgs> handler,
        object sender, TEventArgs e) where TEventArgs : EventArgs
    {
        if (handler != null)
            handler(sender, e);
    }
}

public class EventArgsGeneric<T1> : EventArgs
{
    public EventArgsGeneric(T1 obj1)
    {
        Initialize(obj1);
    }

    public void Initialize(T1 obj1)
    {
        Object1 = obj1;
    }

    public T1 Object1 { get; private set; }
}

public class EventArgsGeneric<T1,T2> : EventArgs
{
    public EventArgsGeneric(T1 obj1,T2 obj2)
    {
        Initialize(obj1, obj2);
    }

    public void Initialize(T1 obj1, T2 obj2)
    {
        Object1 = obj1;
        Object2 = obj2;
    }
   
    


    public T1 Object1 { get; private set; }
    public T2 Object2 { get; private set; }
}

public class EventArgsGeneric<T1, T2,T3> : EventArgs
{
    public EventArgsGeneric(T1 obj1, T2 obj2, T3 obj3)
    {
        Initialize(obj1, obj2,obj3);
    }

    public void Initialize(T1 obj1, T2 obj2, T3 obj3)
    {
        Object1 = obj1;
        Object2 = obj2;
        Object3 = obj3;
    }



    public T1 Object1 { get; private set; }
    public T2 Object2 { get; private set; }
    public T3 Object3 { get; private set; }
}