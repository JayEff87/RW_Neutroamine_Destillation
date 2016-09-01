/*
 * Created by SharpDevelop.
 * User: Jay
 * Date: 31.08.2016
 * Time: 17:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ----------------------------------------------------------------------
// These are RimWorld-specific usings. Activate/Deactivate what you need:
// ----------------------------------------------------------------------
using UnityEngine;         // Always needed
//using VerseBase;         // Material/Graphics handling functions are found here
using Verse;               // RimWorld universal objects are here (like 'Building')
//using Verse.AI;          // Needed when you do something with the AI
//using Verse.Sound;       // Needed when you do something with Sound
//using Verse.Noise;       // Needed when you do something with Noises
using RimWorld;            // RimWorld specific functions are found here (like 'Building_Battery')
//using RimWorld.Planet;   // RimWorld specific functions for world creation
//using RimWorld.SquadAI;  // RimWorld specific functions for squad brains 

namespace Neutroamine_Distilling
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class Distillery : Building_WorkTable
	{
		
		bool destroyedFlag = false;
		String txtStatus = "Distillery_Status";
		bool checkedForTicking = false;
		
		// ===================== Destroy =====================

        /// <summary>
        /// Clean up when this is destroyed
        /// </summary>
        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            // block further ticker work
            destroyedFlag = true;

            base.Destroy(mode);
        }
		
		// ===================== Ticker =====================

        /// <summary>
        /// This is used, when the Ticker in the XML is set to 'Rare'
        /// This is a tick thats done once every 250 normal Ticks
        /// </summary>
        public override void TickRare()
        {
            if (destroyedFlag) // Do nothing further, when destroyed (just a safety)
                return;

            // Don't forget the base work
            base.TickRare();

            // Call work function
            DoTickerWork(250);
        }


        /// <summary>
        /// This is used, when the Ticker in the XML is set to 'Normal'
        /// This Tick is done often (60 times per second)
        /// </summary>
        public override void Tick()
        {
            if (destroyedFlag) // Do nothing further, when destroyed (just a safety)
                return;

            base.Tick();

            // Call work function
            DoTickerWork(1);
        }
        
         // ===================== Main Work Function =====================

        /// <summary>
        /// This will be called from one of the Ticker-Functions.
        /// </summary>
        /// <param name="tickerAmount"></param>
        private void DoTickerWork(int tickerAmount)
        {
            // The following, if activated, creates an entry to the output_log.txt file, so that you can debug something
            //Log.Error("This description will be shown, if active, in the console and always in the output_log.txt");
            if (!UsableNow)
            	Log.Error("Not usable now :o");
            if (!checkedForTicking) {
            	checkedForTicking = true;
            	Log.Error("yup, works");
            }
            
        }
        
        
        // ===================== Inspections =====================

        /// <summary>
        /// This string will be shown when the object is selected (focus)
        /// </summary>
        /// <returns></returns>
        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Add the inspections string from the base
            stringBuilder.Append(base.GetInspectString());

            // Add your own strings (caution: string shouldn't be more than 5 lines (including base)!)
            //stringBuilder.Append("Power output: " + powerComp.powerOutput + " W");
            //stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.Append(txtStatus.Translate() + " ");  // <= TRANSLATION

            // return the complete string
            return stringBuilder.ToString();
        }
	}
}