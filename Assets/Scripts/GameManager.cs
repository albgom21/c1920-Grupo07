﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    UIManager theUIManager;

    private int partesIngrediente = 0;
    private int segs = 5;
    public int monedas = 0;
    private int capsulasG = 6;
    public int mejoraG = 0;
    private int mejoraT = 0;


    private bool gravedad = false;
    private bool tiempo = false;
    private bool escalera = false;
    private bool reapareceEne = false;
    public bool tiendaG = false;
    private bool tiendaT = false;

    private bool suelo;
    private bool paredL;
    private bool paredR;


    void Awake() // Comprobar que solo hay un GameManager
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else Destroy(this.gameObject);

    }
    public void SetSuelo(bool isGrounded)
    {
        suelo = isGrounded;

    }
    public bool GetSuelo()
    {
        return suelo;
    }
    public void SetParedL(bool paredL_)
    {
        paredL = paredL_;

    }
    public bool GetParedL()
    {
        return paredL;
    }

    public void SetParedR(bool paredR_)
    {
        paredR = paredR_;

    }
    public bool GetParedR()
    {
        return paredR;
    }

    public void AddMonedas()
    {
        monedas++;
        if (theUIManager != null)
            theUIManager.UpdateMonedas(monedas);
        Debug.Log(monedas);
    }
    public void ReiniciaMonedas()
    {
        monedas = 0;
        if (theUIManager != null)
            theUIManager.UpdateMonedas(monedas);
    }

    public void SetSegs(int segundos)
    {
        segs = segundos;
        theUIManager.UpdateTiempo(segs);
    }

    public void CambioTiempo()
    {
        if (segs == 5)
        {
            tiempo = !tiempo;      //Invierte tiempo y lo vuelve a invertir después de 5 segundos                       
            InvokeRepeating("Crono", 0f, 1f);
        }
    }

    public void Crono()
    {
        segs -= 1;
        theUIManager.UpdateTiempo(segs);
        if (segs == 0)
        {
            tiempo = false;
            CancelInvoke();
            InvokeRepeating("RecuperarTiempo", 1f, 1.5f);
        }
    }

    public void RecuperarTiempo()
    {
        if (segs != 5)
        {
            segs += 1;
            theUIManager.UpdateTiempo(segs);
        }
        if (segs == 5)
            CancelInvoke();
    }

    public bool Tiempo()
    {
        return tiempo;
    }

    public void SetGravedad(bool active) //Método que recoge booleano del script Gravedad para saber el estado
    {
        gravedad = active;
        SetCapsulasRest(capsulasG - 1);
        theUIManager.UpdateGravedad(capsulasG);
        Debug.Log("CAPSULAS RESTANTES: " + capsulasG);
    }

    public bool GetGravedad() //Método que devuelve el booleano pedido en el método de SetGravedad
    {
        return gravedad;
    }

    public void SetCapsulasRest(int capsG)
    {
        capsulasG = capsG;
        theUIManager.UpdateGravedad(capsulasG);
    }

    public int GetCapsulasRest()
    {
        return capsulasG;
    }

    public void SetEscalera(bool escalera1)
    {
        escalera = escalera1;
    }

    public bool GetEscalera()
    {
        return escalera;
    }

    public void ChangeScene(string sceneName)
    {
        if (sceneName != "")    //Para botón reanudar
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

            tiempo = false;
            gravedad = false;
            escalera = false;

        }

        Time.timeScale = 1;

    }

    public void RecogeIngrediente(int nIngr)
    {
        partesIngrediente++;
        theUIManager.UpdateIngredientes(nIngr);
        Levelfinished();
    }

    public void Levelfinished()
    {
        if (partesIngrediente == 4)
        {
            ChangeScene("Nivel2");
        }
        else if (partesIngrediente == 8)
        {
            ChangeScene("Nivel3");
        }
        else if (partesIngrediente == 12)
        {
            ChangeScene("Nivel4");
        }
    }
    public void SetUIManager(UIManager uim) // Comprobar solo un UI y actualizarlo
    {
        theUIManager = uim;
        theUIManager.UpdateMonedas(monedas);
        theUIManager.UpdateGravedad(capsulasG);
    }

    public void SetReapareceEnemigo(bool _reapareceEne)
    {
        reapareceEne = _reapareceEne;
    }

    public bool GetReapareceEnemigo()
    {
        return reapareceEne;
    }



    public void TiendaGravedad()
    {
        if (monedas >= 1)
        {
            Debug.Log("FFFFFFFFFF");
            mejoraG += 1;
            monedas -= 1;
            if (theUIManager != null)
                theUIManager.UpdateMonedas(monedas);
        }
        if (mejoraG == 3)
        {
            theUIManager.TiendaGravedad();
            capsulasG = 8;
            tiendaG = true;
        }

    }

    public void TiendaTiempo()
    {

    }

    public int GetCapsulasG()
    {
        if (tiendaG)
            return 8;
        else
            return 6;
    }

    public void AnulaMejoras()
    {
        tiendaG = false;
    }
}
