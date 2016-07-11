#include <18F4550.h>
#include <stdlib.h>
#fuses HSPLL,NOWDT,NOPROTECT,NOLVP,NODEBUG,USBDIV,PLL5,CPUDIV1,VREGEN, HS
#use delay(clock=20000000)
#include <usb_cdc.h>
#define LCD_TYPE 1

//#define use_portb_lcd true
#include <LCD.C>

#define LCD_ENABLE_PIN  PIN_D0                                    ////Declaracion pines del PIC para el LCD
#define LCD_RS_PIN      PIN_D1                                    ////
#define LCD_RW_PIN      PIN_D2                                    ////
#define LCD_DATA4       PIN_D4                                    ////
#define LCD_DATA5       PIN_D5                                    ////
#define LCD_DATA6       PIN_D6                                    ////
#define LCD_DATA7       PIN_D7  

#use standard_io(B)
#use standard_io(A)

char ch;
int entero;
int1 control;

void cases()
{
 unsigned long tiempo=0;
 int mult=18;
      switch(ch){  
      
      ////Válvula 1 - Abrir
         case 1:
            if(input(PIN_A0)==1)
            {
               if((input(PIN_A3)==0))
               {
                  output_high(PIN_B4);
                   lcd_gotoxy(1,1);
                   printf(lcd_putc,"%s"," Abriendo VLV 1 ");
                    
                  while((input(PIN_A3)==0) && (mult!=0))
                     {
                    tiempo++;
                     if (tiempo==2000000000)
                        {
                        mult--;
                        tiempo=0;
                        lcd_gotoxy(1,2);
                        printf(lcd_putc,"%s %d"," Tiempo: ", mult);
                        }
                        
                     }
                     //tiempo=0;
                    output_low(PIN_B4);  
                    lcd_gotoxy(1,1);
                    printf(lcd_putc,"%s","                "); 
                    delay_ms(100);
                    lcd_gotoxy(1,1);
                    printf(lcd_putc,"%s","VLV 1: Abierta");  
               }
            }
      break;

      ////Válvula 1 - Cerrar
         case 2:
            if(input(PIN_A0)==1)
            {
               if((input(PIN_A4)==1))
               {
                  output_high(PIN_B5);
                   lcd_gotoxy(1,2);
                   printf(lcd_putc,"%s"," Cerrando VLV 1 ");
                  while(input(PIN_A4)==1)
                     {
                    long tiempo=0;
                    int mult=0;
                    tiempo++;
                     if (tiempo==20000)
                        {
                        mult++; 
                        }        
                     }
                    output_low(PIN_B5);  
                    lcd_gotoxy(1,2);
                    printf(lcd_putc,"%s","                "); 
                    delay_ms(100);
                    lcd_gotoxy(1,2);
                    printf(lcd_putc,"%s","VLV 1: Cerrada");  
               }
            }
      break;

      ////Válvula 2 - Abrir
         case 3:
             if(input(PIN_A5)==1)
                        {
                           if((input(PIN_B1)==1))
                           {
                              output_high(PIN_B6);
                               lcd_gotoxy(1,2);
                               printf(lcd_putc,"%s"," Abriendo VLV 2 ");
                              while(input(PIN_B1)==1)
                                 {
                                long tiempo=0;
                                int mult=0;
                                tiempo++;
                                 if (tiempo==20000)
                                    {
                                    mult++; 
                                    }        
                                 }
                                output_low(PIN_B6);  
                                lcd_gotoxy(1,2);
                                printf(lcd_putc,"%s","                "); 
                                delay_ms(100);
                                lcd_gotoxy(1,2);
                                printf(lcd_putc,"%s","VLV 2: Abierta");  
                           }
                        }
      break;

      ////Válvula 2 - Cerrar
         case 4:
             if(input(PIN_A5)==1)
                        {
                           if((input(PIN_B2)==1))
                           {
                              output_high(PIN_B7);
                               lcd_gotoxy(1,2);
                               printf(lcd_putc,"%s"," Cerrando VLV 2 ");
                              while(input(PIN_B2)==1)
                                 {
                                long tiempo=0;
                                int mult=0;
                                tiempo++;
                                 if (tiempo==20000)
                                    {
                                    mult++; 
                                    }        
                                 }
                                output_low(PIN_B7);  
                                lcd_gotoxy(1,2);
                                printf(lcd_putc,"%s","                "); 
                                delay_ms(100);
                                lcd_gotoxy(1,2);
                                printf(lcd_putc,"%s","VLV 2: Cerrada");  
                           }
                        }

      break; 
      
       case 5:
        output_low(PIN_B0); ///////
       break;
   }    
}

void main()
   { 
      usb_cdc_init();
      usb_init();
      lcd_init();
      lcd_gotoxy(1,1);
//      printf(usb_cdc_putc,"%s", "  --Valvulas--  ");
      printf(lcd_putc,"%s", "  --Valvulas--  ");
      delay_ms(1000);
      lcd_gotoxy(1,2);
//      printf(usb_cdc_putc,"%s","    Welcome!    ");
      printf(lcd_putc,"    Welcome!    ");  


      do{
      usb_task();
      if (usb_enumerated()){
        output_high(PIN_B7); ///////
         if (usb_cdc_kbhit()){
            ch=usb_cdc_getc();
          //  get_string_usb(ch,4);
            if (ch==' ') control=control+1;
         }
          if (control==1){
            printf(usb_cdc_putc,"Comando: %s" ch);
            lcd_gotoxy(1,1);
            printf(lcd_putc,"Comando:%s  ",ch);
            entero=atoi(ch);
       cases();  
          }
       else  
            printf(usb_cdc_putc, "\f ");
         delay_ms(300);   
         }
      }
      while(TRUE);
   }
