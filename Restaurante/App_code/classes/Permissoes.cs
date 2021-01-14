using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Permissoes
{
    public const int caixa   = 1;  // 0b1
    public const int cozinha = 2;  // 0b10
    public const int garcom  = 4;  // 0b100
    public const int gerente = 8;  // 0b1000
    public const int admin   = 16; // 0b10000
    public const int founder = 32; // 0b100000
}