using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
// using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dojodachi.Controllers 
{ 
    public class DojodachiController: Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            int? happiness = HttpContext.Session.GetInt32("happiness");
            if(happiness==null){
                HttpContext.Session.SetInt32("happiness",20);
                happiness=20;
            }
            // else if(happiness>=100){
            //     TempData["won"]="You won!";
            // }
            ViewBag.happiness=happiness;
            int? meal = HttpContext.Session.GetInt32("meal");
            if(meal==null){
                HttpContext.Session.SetInt32("meal",3);
                meal=3;
            }
            ViewBag.meal=meal;
            int? energy = HttpContext.Session.GetInt32("energy");
            if(energy==null){
                HttpContext.Session.SetInt32("energy",50);
                energy=50;
            }
            
            ViewBag.energy=energy;
            int? fullness = HttpContext.Session.GetInt32("fullness");
            if(fullness==null){
                HttpContext.Session.SetInt32("fullness",20);
                fullness=20;
            }
            // else if(fullness>=100){
            //     TempData["won"]="You won!";
            // }
            ViewBag.fullness=fullness;
            if(energy>=100 && fullness>=100 && happiness>=100){
                TempData["won"]="You won!";
            }
            if(fullness==0 || happiness==0){
                TempData["lowhappiness"]="Your Dojodachi is passed away...";
            }
            return View("index");
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed(){
            
            int? meal = HttpContext.Session.GetInt32("meal");
            int? fullness = HttpContext.Session.GetInt32("fullness");
            
            Random fedrand=new Random();
            int ff=fedrand.Next(1,5);
            
            if(meal>0)
            {
                meal-=1;
                int i= Convert.ToInt32(meal);
                HttpContext.Session.SetInt32("meal",i);
                TempData["feed"]=($"You fed your Dojodachi. Dojodachi doesn't like food:(( Meals -1");
                if(ff>1)
                {
                Random random = new Random();
                int num=random.Next(5,11);
                if(fullness>0){
                fullness+=num;
                int h= Convert.ToInt32(fullness);
                HttpContext.Session.SetInt32("fullness",h);
                TempData["feed"]=($"You fed your Dojodachi. Fullness +{num}, Meals -1");
                }
                }
                }
            else if(meal==0){
                TempData["lowmeal"]="There is no meal to feed. Please go and work to earn some food";
            }
            return RedirectToAction ("Index");
        }
        [HttpGet]
        [Route("play")]
        public IActionResult Play(){
            int? energy = HttpContext.Session.GetInt32("energy");
            if(energy>0)
            {
                energy-=5;
                int en = Convert.ToInt32(energy);
                HttpContext.Session.SetInt32("energy",en);
                int? happiness= HttpContext.Session.GetInt32("happiness");
                Random newrand= new Random();
                int nr=newrand.Next(1,5);
                if(nr>1){
                    if(happiness!=null)
                {
                    Random rand= new Random();
                    int happy=rand.Next(5,11);
                    happiness=happiness+ happy;
                    int j= Convert.ToInt32(happiness);
                    HttpContext.Session.SetInt32("happiness",j);
                    TempData["play"]=$"You played with your Dojodachi. Happiness +{happy}, Energy -5";
                }
            }
                TempData["play"]=$"You played with your Dojodachi. Dojodachi doesn't like to play :(, Energy -5";
            }
            
            else if (energy<=0){
                TempData["lowenergy"]="No more energy to keep playing :((";
            }
            
            return RedirectToAction ("Index");
        }
        [HttpGet]
        [Route("work")]
        public IActionResult Work(){
            int? energy = HttpContext.Session.GetInt32("energy");
            int? meal = HttpContext.Session.GetInt32("meal");
            Random random = new Random();
            int food=random.Next(1,4);
            if(energy>0){
                energy-=5;
                int e = Convert.ToInt32(energy);
                HttpContext.Session.SetInt32("energy",e);
                TempData["work"]=$"You  Dojodachi worked. Meals +{food}, Energy -5";
                
                if(meal>=0){
                meal+=food;
                int m = Convert.ToInt32(meal);
                HttpContext.Session.SetInt32("meal",m);
            }
            }
            else if (energy<=0){
                TempData["lowenergy"]="No more energy to keep working:((";
            }
            return RedirectToAction ("Index");
        }
         

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep(){
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? happiness= HttpContext.Session.GetInt32("happiness");
            int? energy = HttpContext.Session.GetInt32("energy");
            
            if (happiness<=5 || fullness<=5){
                HttpContext.Session.SetInt32("happiness",0);
                HttpContext.Session.SetInt32("fullness",0);

                TempData["lowhappiness"]="Your Dojodachi is passed away...";
                //Console.WriteLine(TempData["lowhappiness"]);
            }
            else if(fullness!=null){
                fullness-=5;
                int f = Convert.ToInt32(fullness);
                HttpContext.Session.SetInt32("fullness",f);
                TempData["sleep"]="Your Dojodachi slept. Happiness -5,Fullness -5, Energy +15";
                
            if(happiness>0){
                happiness-=5;
                int h = Convert.ToInt32(happiness);
                HttpContext.Session.SetInt32("happiness",h);
                }
            if(energy>=0){
                energy+=15;
                int ener =Convert.ToInt32(energy);
                HttpContext.Session.SetInt32("energy",ener);
            }
            
            }
        return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }





    } 


}