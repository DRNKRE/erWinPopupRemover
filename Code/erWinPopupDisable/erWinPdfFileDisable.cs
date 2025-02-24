//#define DEBUG_SEARCH //Uncommend to debug the search function using debug writes  !!!WARNING SLOW!!!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace erWinBatchPopupDisable {
    
    internal interface IerWinPdfFilePopupDisableClass {

        event EventHandler<erWinPdfFilePopupDisableEvent> StatusUpdate; //Called when the status of the system has changed
        
        int fileToConvertCount{ get; }  //The total number of files

        int currentFileCount{ get; } //The current file count completed


        //Name : Load Directory
        //Description : Will scan a directory for all PDF files and load them into a list.
        //  If a directory is already loaded, this will do nothing and exit.
        //  If a task is currently running, this will do nothing and exit.
        //
        //Args: 
        //  string dirPath : The path to the directory to be scanned
        //  bool recursive :
        //      TRUE - search the dirPath and all subdirectories.
        //      FALSE - search only the dirPath
        //
        //Return : int
        //  count = number of files found
        int LoadDirectory(string dirPath, bool recursive);
        

        //Name : Start
        //Description : Launches a new thread and converts all the files on the current list.
        //  if a conversion is currently running it will exit and do nothing.
        //
        //Args: null
        //
        //Return : void
        void Start();

        //Name : Reset
        //Description : Resets the current list.
        //  This requires any currently running tasks to be stopped.  Will do nothing
        //  if a task is running.
        //
        //Args: null
        //
        //Return : void
        void Reset();

        //Name : Stop
        //Description : Will stop the current conversion process if one is running.  Otherwise
        //      It will do nothing.
        //
        //Args : null
        //
        //Return : void
        void Stop();

    }

    internal class erWinPdfFilePopupDisableEvent {
        public string msg;
        public int percent;
    }


    internal class erWinPdfFilePopupDisableClass : IerWinPdfFilePopupDisableClass {


        //################################################################################
        //  Public
        //################################################################################
        public event EventHandler<erWinPdfFilePopupDisableEvent> StatusUpdate;
        public int fileToConvertCount { get { return m_filesToConvert.Count; } }
        public int currentFileCount { get { return m_currentFileIndex; } }

        //----------------------------------------------------------------------------------Load Directory
        public int LoadDirectory(string dirPath, bool recursive) {
            //Check if files were already loaded
            if (fileToConvertCount > 0) {
                erWinPdfFilePopupDisableEvent eArgs = new erWinPdfFilePopupDisableEvent();
                eArgs.msg = "Failed to Load Directory. Directory Already Loaded.";
                if (StatusUpdate != null) StatusUpdate(this, eArgs);
                return 0;
            }

            //Check if we're currently running a task
            if (m_thread != null) {
                erWinPdfFilePopupDisableEvent eArgs = new erWinPdfFilePopupDisableEvent();
                eArgs.msg = "Failed to Load Directory. Task is currently being executed.";
                if (StatusUpdate != null) StatusUpdate(this, eArgs);
                return 0;
            }

            //Add all the files from this directory
            m_SearchDirectoryForPdfs(dirPath, recursive);

            //Return count
            return fileToConvertCount;
        }

        //---------------------------------------------------------------------------------- Start
        public void Start() {

            //Check if we need to clean up a previous run
            if (!m_running && m_thread != null) {
                m_thread.Join();
                m_thread = null;
            }

            //Check if something is already running
            if (m_thread != null) {
                erWinPdfFilePopupDisableEvent eArgs = new erWinPdfFilePopupDisableEvent();
                eArgs.msg = "Failed to start conversion. Conversion in-progress.";
                if (StatusUpdate != null) StatusUpdate(this, eArgs);
                return;
            }

            //Check if there are any files to convert
            if (fileToConvertCount <= 0) {
                erWinPdfFilePopupDisableEvent eArgs = new erWinPdfFilePopupDisableEvent();
                eArgs.msg = "Failed to start conversion. Nothing to convert.";
                if (StatusUpdate != null) StatusUpdate(this, eArgs);
                return;
            }

            ThreadStart ts = new ThreadStart(m_ConvertFiles);
            m_thread = new Thread(ts);
            m_thread.Start();
        }

        //---------------------------------------------------------------------------------- Reset
        public void Reset() {

            //Check if something is already running
            if (m_thread != null) {
                erWinPdfFilePopupDisableEvent eArgs = new erWinPdfFilePopupDisableEvent();
                eArgs.msg = "Failed to Reset. Conversion in-progress.";
                if (StatusUpdate != null) StatusUpdate(this, eArgs);
                return;
            }

            m_filesToConvert.Clear();

        }

        //---------------------------------------------------------------------------------- Stop
        public void Stop() {
            erWinPdfFilePopupDisableEvent eArgs = new erWinPdfFilePopupDisableEvent();

            //Check if there is something to stop
            if (m_thread == null) {
                eArgs.msg = "Failed to start conversion. Conversion not running.";
                if (StatusUpdate != null) StatusUpdate(this, eArgs);
                return;
            }

            eArgs.msg = "Stopping...";
            if (StatusUpdate != null) StatusUpdate(this, eArgs);

            m_running = false;
        }


        //################################################################################
        //  Private
        //################################################################################
        List<string> m_filesToConvert = new List<string>(); //List of files to be converted
        int m_currentFileIndex;
        bool m_running;
        Thread? m_thread;

        //----------------------------------------------------------------------------------Create Event Data
        //Name : Create Event Data
        //Description : Calculates the current percent and adds the message to a event structure
        //      then returns that structure.
        //
        //Arguments :
        //  string msg - The message to be inserted into the event args.
        //
        //Return : erWinPdfFilePopupDisableEvent
        //  New event structure with information already populated.
        private erWinPdfFilePopupDisableEvent m_CreateEventData(string msg) {
            erWinPdfFilePopupDisableEvent eArgs = new erWinPdfFilePopupDisableEvent();

            //Avoid divide by zero
            if (fileToConvertCount == 0)
                eArgs.percent = 0;
            else
                eArgs.percent = ((currentFileCount) * 100) / fileToConvertCount;
                
            eArgs.msg = msg;

            return eArgs;
        }

        //----------------------------------------------------------------------------------Convert Files
        //Name : Convert Files
        //Description : Loops through all the files on the list and converts each one.
        //
        //Arguments : null
        //
        //Return : Void
        private void m_ConvertFiles() {
            m_running = true;

            if (StatusUpdate != null) StatusUpdate(this, m_CreateEventData("Conversion Started..."));

            string tempMessage = "";
            m_currentFileIndex = 0;
            foreach (string file in m_filesToConvert) {

                if (!m_running) break; //Exit when asked

                //Update status
                tempMessage = "Convert " + Path.GetFileNameWithoutExtension(file);
                if (StatusUpdate != null) StatusUpdate(this, m_CreateEventData(tempMessage));
                
                //Disable popup in file
                m_DisablePopupFile(file);
                
                //Update index
                m_currentFileIndex++;
            }

            m_running = false;

            //Launch complete event
            if (StatusUpdate != null) StatusUpdate(this, m_CreateEventData("Done."));
        }

        //----------------------------------------------------------------------------------Search Directory for PDFs
        //Name : Search Directory for PDFs
        //Description : Adds all the PDFs that it finds in the directory to the list
        //      of files to be converted.
        //
        //Arguments :
        //  string dirPath : The path to the directory to be searched.
        //  bool recursive : TRUE - Open subdirectories to the current directory.
        //                   FALSE - Only look in the current directory.
        //
        //Return : Void
        private void m_SearchDirectoryForPdfs(string dirPath, bool recursive) {
            string[] files = Directory.GetFiles(dirPath, "*.pdf");

            foreach (string file in files)
                m_filesToConvert.Add(file);
            
            //Stop if not recursive
            if (!recursive) return; 

            string[] directories = Directory.GetDirectories(dirPath);
            foreach (string directory in directories) {
                m_SearchDirectoryForPdfs(directory, recursive);
            }
        }


        //----------------------------------------------------------------------------------Disable Popup [in] File
        //Name : Disable Popup [in] File
        //Description : For a given file, It will be loaded into memory.  The two
        //      strings "showPopup" and "showUSApopup" searched for.  If found,
        //      they'll be changed from true to false.  This will disable the
        //      popup in the file.
        //
        //Arguments :
        //  string filePath : a file path to a PDF file that is presumed to be
        //      the drawing.
        //
        //Return : Void
        private void m_DisablePopupFile(string filePath) {
            if (!File.Exists(filePath))
                return;

            byte[] fileData = File.ReadAllBytes(filePath);
            bool fileChanged = false;

            int catchi = m_SearchForStringPattern(ref fileData, "showPopup=true");

            m_ReplaceStringPattern(ref fileData, "showPopup=true", "showPopup=false");
            m_ReplaceStringPattern(ref fileData, "showUSAPopup=true", "showUSAPopup=false");

            File.WriteAllBytes(filePath, fileData);
        }

        //----------------------------------------------------------------------------------Search For String Pattern
        //Name : Search For String Pattern
        //Description : Searches for the first instance of searchStr in the character array src.
        //
        //Arguments :
        // byte[]* src - source character array as byte array to be searched.
        // string searchStr - the string that will be search for in the character array
        //
        //Return : Int
        // < 0 - Error or nothing found
        // else - Index of search pattern
        private int m_SearchForStringPattern(ref byte[] src, string? searchStr) {
            if (searchStr == null) return -1;

            int match_i = -1;
            int j = 0;

            char[] searchArr = searchStr.ToCharArray();

#if DEBUG_SEARCH && DEBUG
            Debug.WriteLine("Beginning String Search for " + searchStr);
            Debug.WriteLine("[i] char [j] char [MATCH]");
#endif

            //Loop through all the characters of the source c_string
            for (int i = 0; i < src.Length; i++) {

#if DEBUG_SEARCH && DEBUG
                Debug.WriteLine("[" + i + "] " + (char)src[i] + " [" + j + "] " + searchArr[j] + " [" + ((src[i] == searchArr[j]) ? "T" : "F") + "]");
#endif

                //The current index in the source
                //  matches the current index of the searchStr
                //T: Look at the next character in the searchStr
                //F: Reset searchStr and check again to avoid missing
                if (src[i] == searchArr[j]) {
                    if (j == 0) match_i = i;
                    j++;
                } else {
                    i = i - ( (j != 0) ? 1 : 0 ); //Move index back one to check this index again if our sub-index is not 0
                    match_i = -1;
                    j = 0;
                }

                //The searchStr index is maxes out therefore we found
                //  the string.
                if (j == searchArr.Length) {
#if DEBUG_SEARCH && DEBUG
                    Debug.WriteLine("Match Found.");
#endif
                    return match_i;
                }


            }

            return -1;
        }


        //----------------------------------------------------------------------------------Replace String Pattern
        //Name : Replace String Pattern
        //Description : Given a character array as a byte array, the function will find the
        //      first instance of searchStr and replace it with replaceStr.  The length of
        //      the array will be adjusted accordingly.
        //
        //Arguments :
        //  byte[]* ba - Character array as a reference to a byte array
        //  string searchStr - The string to be search for in the byte array
        //  string replaceStr - The string to replace the search string in the byte array.
        //
        //Return :
        //  TRUE - success;
        //  FALSE - "searchStr" was not found in the byte array.
        private bool m_ReplaceStringPattern(ref byte[] ba, string searchStr, string replaceStr) {

            int starti = m_SearchForStringPattern(ref ba, searchStr);
            int endi = starti + replaceStr.Length - 1;

            if (starti == -1) return false;

            byte[] newByteArray;
            int deltaSize = replaceStr.Length - searchStr.Length;
            char[] destByte = replaceStr.ToCharArray();

            newByteArray = new byte[ba.Length + deltaSize];

            //copy original data
            for (int j = 0; j < starti; j++)
                newByteArray[j] = ba[j];

            //copy new string
            for (int j = starti; j <= endi; j++) {
                newByteArray[j] = (byte)destByte[j - starti];
            }

            //copy remainder
            for (int j = endi + 1; j < newByteArray.Length; j++) {
                newByteArray[j] = ba[j - deltaSize];
            }

            ba = newByteArray;
            return true;
        }

    }
}
