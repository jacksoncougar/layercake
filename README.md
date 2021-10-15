# LayerCake

Create Karabiner-Element complex modifications from a simple Language

## Language

The language is aimed at simple key remapping and key layers.

In the current version of the Language you can:
- Map one key to another
- Swap two keys
- Create a layer 
- Toggle a layer via a key

### Mapping a key to another key

To map a key to another key you can use the `is` statement. This statement has the form:
```
a is b
```
This maps the `a` key to the `b` key. 

**Note: The original `b` key remains unaffected and the keyboard will now have two `b` keys.**

### Swapping two keys

To swap two keys you can use the `swap` statement. This statement has the form 
```
swap a and b
```

This maps the `a` key to the `b` key and vice versa.

**Note: This effects both keys on the keyboard and there will only be one `a` or `b` key.**


### Creating a layer of keys

To group a collection of key mapping together you can use the `layer` statement. This statement has the form:

```
my_layer_name layer
  
done
```

A layer can contain `is` and `swap` statements. 
- When the layer is active the remappings in the layer are turned on. 
- When the layer remappings are turned off.

### Example

This simple layer for common navigation written in the Language

```
up_arrow is nothing
left_arrow is nothing
down_arrow is nothing
right_arrow is nothing

when bundle is www.parsec.tv
    swap left_command and left_alt 
done

navigation layer 
    i is up_arrow
    j is left_arrow
    k is down_arrow
    l is right_arrow
    h is home
    n is end
    s is left_shift
    a is left_control
    open_bracket is delete_forward
    quote is delete_or_backspace
done
```
Is turned into a set of Karabiner.json complex modifications:
```
{
    "description": "Keyboard: caps_lock toggles navigation",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [],
        "from": {
        "key_code": "caps_lock",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "set_variable": {
            "name": "navigation",
            "value": 1
            }
        }
        ],
        "to_after_key_up": {
        "set_variable": {
            "name": "navigation",
            "value": 0
        }
        }
    }
    ]
},
{
    "description": "Keyboard: up_arrow is nothing",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [],
        "from": {
        "key_code": "up_arrow",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": []
    }
    ]
},
{
    "description": "Keyboard: left_arrow is nothing",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [],
        "from": {
        "key_code": "left_arrow",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": []
    }
    ]
},
{
    "description": "Keyboard: down_arrow is nothing",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [],
        "from": {
        "key_code": "down_arrow",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": []
    }
    ]
},
{
    "description": "Keyboard: right_arrow is nothing",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [],
        "from": {
        "key_code": "right_arrow",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": []
    }
    ]
},
{
    "description": "Keyboard: left_command is left_alt",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [],
        "from": {
        "key_code": "left_command",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "left_alt"
        }
        ]
    }
    ]
},
{
    "description": "Keyboard: left_alt is left_command",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [],
        "from": {
        "key_code": "left_alt",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "left_command"
        }
        ]
    }
    ]
},
{
    "description": "navigation: i is up_arrow",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "i",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "up_arrow"
        }
        ]
    }
    ]
},
{
    "description": "navigation: j is left_arrow",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "j",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "left_arrow"
        }
        ]
    }
    ]
},
{
    "description": "navigation: k is down_arrow",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "k",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "down_arrow"
        }
        ]
    }
    ]
},
{
    "description": "navigation: l is right_arrow",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "l",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "right_arrow"
        }
        ]
    }
    ]
},
{
    "description": "navigation: h is home",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "h",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "home"
        }
        ]
    }
    ]
},
{
    "description": "navigation: n is end",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "n",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "end"
        }
        ]
    }
    ]
},
{
    "description": "navigation: s is left_shift",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "s",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "left_shift"
        }
        ]
    }
    ]
},
{
    "description": "navigation: a is left_control",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "a",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "left_control"
        }
        ]
    }
    ]
},
{
    "description": "navigation: open_bracket is delete_forward",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "open_bracket",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "delete_forward"
        }
        ]
    }
    ]
},
{
    "description": "navigation: quote is delete_or_backspace",
    "manipulators": [
    {
        "type": "basic",
        "conditions": [
        {
            "type": "variable_if",
            "name": "navigation",
            "value": 1
        }
        ],
        "from": {
        "key_code": "quote",
        "modifiers": {
            "mandatory": [],
            "optional": [
            "any"
            ]
        }
        },
        "to": [
        {
            "key_code": "delete_or_backspace"
        }
        ]
    }
    ]
}

```