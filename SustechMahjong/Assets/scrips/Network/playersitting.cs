using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class playersetting
{
    public int playerseat;
    public CardObject[] handcard = new CardObject[13];
    public Vector3 handposition;
    public Vector3 dropzone;
    public Vector3 outzone;
    public Vector3 getpos;
    public Quaternion faceRotation;
    public Quaternion deskRotation;

    public abstract Vector3 gethandposorder(int pos);
    public abstract Vector3 getgetpos();
    public abstract Vector3 getdroppos();
    public abstract Vector3 getoutpos();

    public abstract void nextdrop();
    public abstract void lastdrop();

    public void clearhandcard()
    {
        handcard = new CardObject[13];
    }
}


class Nextplayer : playersetting
{
    public Nextplayer() : base()
    {
        base.playerseat = 1;
        base.deskRotation = new Quaternion(0.5f, 0.5f, 0.5f, -0.5f);
        base.faceRotation = new Quaternion(0, -0.7f, 0, 0.7f);
        base.handposition = new Vector3(-0.289f, 0.015f, 0.16f);
        base.dropzone = new Vector3(-0.105f, 0.007f, 0.06f);
        base.outzone = new Vector3(-0.336f, 0.007f, 0.28f);
        base.getpos = new Vector3(-0.289f, 0.015f, -0.182f);

    }

    public override Vector3 getdroppos()
    {
        return dropzone;
    }

    public override Vector3 getgetpos()
    {
        return getpos;
    }

    public override Vector3 gethandposorder(int pos)
    {
        Vector3 result = handposition;
        result.z -= 0.023f * pos;
        return result;
    }

    public override Vector3 getoutpos()
    {
        Vector3 result = outzone;
        outzone.z += 0.024f;
        return result;
    }

    public override void lastdrop()
    {
        dropzone.z += 0.024f;
        if (dropzone.z > 0.06)
        {
            dropzone.z = -0.06f;
            dropzone.x += 0.034f;
        }
    }

    public override void nextdrop()
    {
        dropzone.z -= 0.024f;
        if (dropzone.z < -0.06)
        {
            dropzone.z = 0.06f;
            dropzone.x -= 0.034f;
        }
    }
}

class Frontplayer : playersetting
{
    public Frontplayer() : base()
    {
        base.playerseat = 1;
        base.deskRotation = new Quaternion(-0.5f, 0.5f, 0.5f, 0.5f);
        base.faceRotation = new Quaternion(0, 0.7f, 0, 0.7f);
        base.handposition = new Vector3(0.28f, 0.015f, -0.16f);
        base.dropzone = new Vector3(0.09f, 0.007f, -0.06f);
        base.outzone = new Vector3(0.33f, 0.007f, 0.339f);
        base.getpos = new Vector3(0.28f, 0.015f, 0.175f);

    }

    public override Vector3 getdroppos()
    {
        return dropzone;
    }

    public override Vector3 getgetpos()
    {
        return getpos;
    }

    public override Vector3 gethandposorder(int pos)
    {
        Vector3 result = handposition;
        result.z += 0.023f * pos;
        return result;
    }

    public override Vector3 getoutpos()
    {
        Vector3 result = outzone;
        outzone.z -= 0.024f;
        return result;
    }

    public override void lastdrop()
    {
        dropzone.z -= 0.024f;
        if (dropzone.z < -0.06)
        {
            dropzone.z = 0.06f;
            dropzone.x -= 0.034f;
        }
    }

    public override void nextdrop()
    {
        dropzone.z += 0.024f;
        if (dropzone.z > 0.06)
        {
            dropzone.z = -0.06f;
            dropzone.x += 0.034f;
        }
       
    }
}

class Antiplayer : playersetting
{
    public Antiplayer() : base()
    {
        base.playerseat = 1;
        base.deskRotation = new Quaternion(0, 0.7f, 0.7f, 0);
        base.faceRotation = new Quaternion(0, -1, 0, 0);
        base.handposition = new Vector3(-0.16f, 0.015f, -0.26f);
        base.dropzone = new Vector3(-0.06f, 0.007f, -0.09f);
        base.outzone = new Vector3(-0.336f, 0.007f, 0.328f);
        base.getpos = new Vector3(0.188f, 0.015f, -0.261f);

    }

    public override Vector3 getdroppos()
    {
        return dropzone;
    }

    public override Vector3 getgetpos()
    {
        return getpos;
    }

    public override Vector3 gethandposorder(int pos)
    {
        Vector3 result = handposition;
        result.x += 0.023f * pos;
        return result;
    }

    public override Vector3 getoutpos()
    {
        Vector3 result = outzone;
        outzone.x -= 0.024f;
        return result;
    }

    public override void lastdrop()
    {
        dropzone.x -= 0.024f;
        if (dropzone.x < -0.06)
        {
            dropzone.x = 0.06f;
            dropzone.z -= 0.034f;
        }
        
    }

    public override void nextdrop()
    {
        dropzone.x += 0.024f;
        if (dropzone.x > 0.06)
        {
            dropzone.x = -0.06f;
            dropzone.z += 0.034f;
        }
    }
}

class Thisplayer : playersetting
{
    public Thisplayer() : base()
    {
        base.playerseat = 1;
        base.deskRotation = new Quaternion(-0.7f, 0, 0, 0.7f);
        base.faceRotation = new Quaternion(-0.5f, 0, 0, 0.9f);
        base.handposition = new Vector3(0.14f , 0.285f, 0.377f);
        base.dropzone = new Vector3(0.06f, 0.007f, 0.09f);
        base.outzone = new Vector3(-0.32f, 0.007f, 0.278f);
        base.getpos = new Vector3(-0.19f, 0.27f, 0.29f);

    }

    public override Vector3 getdroppos()
    {
        return dropzone;
    }

    public override Vector3 getgetpos()
    {
        return getpos;
    }

    public override Vector3 gethandposorder(int pos)
    {
        Vector3 result = handposition;
        result.x -= 0.023f * pos;
        return result;
    }

    public override Vector3 getoutpos()
    {
        Vector3 result = outzone;
        outzone.x += 0.024f;
        return result;
    }

    public override void lastdrop()
    {
        dropzone.x -= 0.024f;
        if (dropzone.x < -0.06)
        {
            dropzone.x = 0.06f;
            dropzone.z += 0.034f;
        }
    }

    public override void nextdrop()
    {
        dropzone.x += 0.024f;
        if (dropzone.x > 0.06)
        {
            dropzone.x = -0.06f;
            dropzone.z -= 0.034f;
        }
    }
}