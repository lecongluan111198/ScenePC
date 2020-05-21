﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractComponent
{
    protected int id;
    protected string name;

    public int Id { get => id; }
    public string Name { get => name; }

    protected AbstractComponent(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    //public AbstractComponent()
    //{
    //    id = 0;
    //    name = "";
    //}

    public abstract void updateInfomation(Component component);

    public abstract Type getType();
}
