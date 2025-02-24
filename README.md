# erWin Popup Remover

## What is erWin?

Volkswagen AG, often shortened as VAG, has an amazing resource called erWin. This allows customers, for a fee, to view and download the service manuals for their Volkswagen or Audi vehicles.

| Manufacturer | Link |
|-------------:|------|
| Audi | https://audi.erwin-store.com/erwin/showHome.do |
| VW | https://volkswagen.erwin-store.com/erwin/showHome.do |


## What's the problem?

In the US, this comes with a caveat.  Each PDF has javascript embedded that causes a popup and a disclaimer agreement to appear.  Not every PDF viewer implements javascript correctly and these scripts.  This causes the document to get stuck at the disclaimer agreement.  This can also be annoying when switching between documents while diagnosing the car.

## What's the solution?

Inside the PDF there are two variable declarations, `showUSAPopup` and `showPopup`.  By default, these variables are set to true.  Change these variables to false and the popup and disclaimer will no longer show up.

## What solution does this app offer?

Per vehicle, there are many PDF documents covering a range of items; wiring diagrams, engines, brakes, etc.  Turning off the pop-up in all the documents can be a hassle.  This may be especially true if you have more than one vehicle.  This program will recursively scan a directory for PDF files and find the variables `showUSAPopup` and `showPopup` and change them to false.

## How to use?

### Step 1

Sign up for erWin and retrieve your service manuals.

### Step 2 

Organize your service manuals.  Example shown below.

`2000 AUDI A4`\
`├───Repair Manual`\
`│   │   D3E80022F6F-Wheels_and_Tires_Guide.pdf`\
`│   │   D3E80023161-Wheel_and_Tire_Guide_General_Information.pdf`\
`│   │   D3E800779B9-Suspension__Wheels__Steering.pdf`\
`│   │   D3E80079193-Brake_System.pdf`\
`│   │   D3E800C1F19-Brake_System_On_Board_Diagnostic.pdf`\
`│   │`\
`│   ├───Body`\
`│   │       D3E80002052-Body_Repair.pdf`\
`│   │       D3E800779BA-Body_Interior.pdf`\
`│   │       D3E800779BB-Body_Exterior.pdf`\
`│   │       D3E800C1F18-Body_On_Board_Diagnostic.pdf`\
`│   │       D4B805DE1F0-Body_Repair__Body_Collision_Repair.pdf`\
`│   │`\
`│   ├───Chassis`\
`│   ├───Drivetrain`\
`│   │       D3E800779BF-Automatic_Transmission.pdf`\
`│   │       D3E800E3EF5-Automatic_Transmission_Internal_Components.pdf`\
`│   │       D3E804C9D51-Manual_Transmission.pdf`\
`...`\

### Step 3

Archive your service manuals.  As with any automated conversion process, make sure you archive of the original before running the program.

`2000 Audi A4.zip`

### Step 4

Run `erWin Popup Remover` then either enter your root directory of your manuals or select browse and select the root directory of your manuals.  The root directory is the highest-level directory that contains your manuals and the sub-directory that contains the manuals and no higher.  If you have more than one directory inside of the root directory, you will want to keep `recursive` checked.  Recursive, in this case, means the program will search your current directory and all directories inside that directory.

### Step 5

Press Go.  After a few moments all the PDFs in your directory will have the pop-ups turned off.
