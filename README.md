# Starbound Directive Generator
Compares a recolored image with the original image, and generates a replace directive for it.

## Installation
* Download the latest release from the [Releases](https://github.com/Silverfeelin/Starbound-DirectiveGenerator/releases) page.
* Unpack the contents of the zipped release to a folder.

## Notes
* Images should be recolored using external software. Some great applications are [Paint.Net](http://www.getpaint.net/index.html) and [GIMP](https://www.gimp.org/).
* A color should only ever be replaced by one other color.
 * In Paint.Net, I recommend the below settings for the Paint Bucket tool.
 
![](https://raw.githubusercontent.com/Silverfeelin/Starbound-DirectiveGenerator/master/readme/pdn-fill.png)

## Usage
* Drag two valid image files of equal dimensions on top of the application.
* Confirm the comparison order.
 * <kbd>Y</kbd> Confirm the given comparison order (`A to B`).
 * <kbd>N</kbd> Switch the comparison order (`B to A`).
 * <kbd>Esc</kbd> Cancel the action.
* Paste the contents of your Clipboard wherever you want <kbd>Ctrl+V</kbd>. The directives string is automatically copied to your Clipboard.

![Drag 'n Drop](https://raw.githubusercontent.com/Silverfeelin/Starbound-DirectiveGenerator/master/readme/usage.png)  
*Opening the application using two images.*

![Console](https://raw.githubusercontent.com/Silverfeelin/Starbound-DirectiveGenerator/master/readme/console.png)
*Confirming the image order in the application.*