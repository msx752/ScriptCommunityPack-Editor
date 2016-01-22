using System.Collections.Generic;

namespace FastColoredTextBoxNS.Render
{
    public class ScriptCommunityPack
    {
        public static DeclarationAuto[] declaration = new DeclarationAuto[]
        {
            new DeclarationAuto("[AREADEF a_^]") { ImageIndex=1 },
            new DeclarationAuto("[CHARDEF c_^]") { ImageIndex=1 },
            new DeclarationAuto("[ROOMDEF a_^]") { ImageIndex=1 },
            new DeclarationAuto("[SKILL ^]") { ImageIndex=1 },
            new DeclarationAuto("[SKILLCLASS ^]") { ImageIndex=1 },
            new DeclarationAuto("[DIALOG d_^]") { ImageIndex=1 },
            new DeclarationAuto("[EVENTS e_^]") { ImageIndex=1 },
            new DeclarationAuto("[ITEMDEF i_^]") { ImageIndex=1 },
            new DeclarationAuto("[MENU m_^]") { ImageIndex=1 },
            new DeclarationAuto("[REGIONTYPE r_^]") { ImageIndex=1 },
            new DeclarationAuto("[SKILLMENU sm_^]") { ImageIndex=1 },
            new DeclarationAuto("[SPAWN ^]") { ImageIndex=1 },
            new DeclarationAuto("[SPELL ^]") { ImageIndex=1 },
            new DeclarationAuto("[TYPEDEF t_^]") { ImageIndex=1 },
            new DeclarationAuto("[FUNCTION f_^]") { ImageIndex=1 },
            new DeclarationAuto("[EOF]^") { ImageIndex=1 },
        };

        public static MethodAuto[] keywords = new MethodAuto[0];

        public static MethodAuto[] methods = new MethodAuto[]
        {
            new MethodAuto("act") { ImageIndex=2 },
            new MethodAuto("attr") { ImageIndex=2 },
            new MethodAuto("body") { ImageIndex=2 },
            new MethodAuto("dex") { ImageIndex=2 },
            new MethodAuto("findcont") { ImageIndex=2 },
            new MethodAuto("findid") { ImageIndex=2 },
            new MethodAuto("findlayer") { ImageIndex=2 },
            new MethodAuto("findtype") { ImageIndex=2 },
            new MethodAuto("id") { ImageIndex=2 },
            new MethodAuto("int") { ImageIndex=2 },
            new MethodAuto("ischar") { ImageIndex=2 },
            new MethodAuto("iscont") { ImageIndex=2 },
            new MethodAuto("isevent") { ImageIndex=2 },
            new MethodAuto("isgm") { ImageIndex=2 },
            new MethodAuto("isitem") { ImageIndex=2 },
            new MethodAuto("isneartype") { ImageIndex=2 },
            new MethodAuto("istevent") { ImageIndex=2 },
            new MethodAuto("mana") { ImageIndex=2 },
            new MethodAuto("maxhits") { ImageIndex=2 },
            new MethodAuto("maxmana") { ImageIndex=2 },
            new MethodAuto("maxstam") { ImageIndex=2 },
            new MethodAuto("maxweight") { ImageIndex=2 },
            new MethodAuto("memoryfindtype") { ImageIndex=2 },
            new MethodAuto("memoryfind") { ImageIndex=2 },
            new MethodAuto("message") { ImageIndex=2 },
            new MethodAuto("messageua") { ImageIndex=2 },
            new MethodAuto("modar") { ImageIndex=2 },
            new MethodAuto("moddex") { ImageIndex=2 },
            new MethodAuto("modint") { ImageIndex=2 },
            new MethodAuto("modmaxweight") { ImageIndex=2 },
            new MethodAuto("modstr") { ImageIndex=2 },
            new MethodAuto("obody") { ImageIndex=2 },
            new MethodAuto("odex") { ImageIndex=2 },
            new MethodAuto("oint") { ImageIndex=2 },
            new MethodAuto("oskin") { ImageIndex=2 },
            new MethodAuto("ostr") { ImageIndex=2 },
            new MethodAuto("owner") { ImageIndex=2 },
            new MethodAuto("p") { ImageIndex=2 },
            new MethodAuto("region") { ImageIndex=2 },
            new MethodAuto("room") { ImageIndex=2 },
            new MethodAuto("sector") { ImageIndex=2 },
            new MethodAuto("stam") { ImageIndex=2 },
            new MethodAuto("str") { ImageIndex=2 },
            new MethodAuto("home") { ImageIndex=2 },
            new MethodAuto("hits") { ImageIndex=2 },
            new MethodAuto("memoryfindtype") { ImageIndex=2 }
        };

        public static List<PopupToolTip> KeywordsInformation = new List<PopupToolTip>();

        public static SnippetAuto[] snippets = new SnippetAuto[]
        {
            new SnippetAuto("if (<^>)\n\nendif") { ImageIndex=1 },
            new SnippetAuto("if (<^>)\n\nelseif (<>)\n\nelse\n\nendif") { ImageIndex=1 },
            new SnippetAuto("for ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forcharlayer ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forcharmemorytype ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forchars ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forclients ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forplayers ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forcont ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forcontid ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forconttype ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forinstances ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("foritems ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("forobjs ^\n\nendfor") { ImageIndex=1 },
            new SnippetAuto("while (<^>)\n\nendwhile") { ImageIndex=1 },
            new SnippetAuto("begin\n^\nend") { ImageIndex=1 },
            new SnippetAuto("dorand ^\n\nenddo") { ImageIndex=1 },
            new SnippetAuto("<eval <^>>") { ImageIndex=1 },
            new SnippetAuto("<floatval <^>>") { ImageIndex=1 },
            new SnippetAuto("<qval <^>>") { ImageIndex=1 },
            new SnippetAuto("<uval <^>>") { ImageIndex=1 },
            new SnippetAuto("<fval <^>>") { ImageIndex=1 },
            new SnippetAuto("<feval <^>>") { ImageIndex=1 },
            new SnippetAuto("<fhval <^>>") { ImageIndex=1 },
            new SnippetAuto("<hval <^>>") { ImageIndex=1 },
            new SnippetAuto("strcmp(^)") { ImageIndex=1 },
            new SnippetAuto("strcmpi(^)") { ImageIndex=1 },
            new SnippetAuto("strindexof(^)") { ImageIndex=1 },
            new SnippetAuto("strlen(^)") { ImageIndex=1 },
            new SnippetAuto("strmatch(^)") { ImageIndex=1 },
            new SnippetAuto("strregex(^)") { ImageIndex=1 },
        };

        public static void LOAD()
        {
            #region keywordsInformation

            KeywordsInformation.Add(new PopupToolTip() { Name = "dorigin", Parameters = "x,y", Comment = "Sets the origin coordinates for dynamically positioned elements.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nomove", Parameters = "", Comment = "Prevents the dialog from being moved around the screen.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "noclose", Parameters = "", Comment = "Prevents the dialog from being closed when right-clicked.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nodispose", Parameters = "", Comment = "Prevents the dialog from being closed by the \"Close Dialogs\" macro.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "page", Parameters = "num", Comment = "For multi tab dialogs.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog, PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "checkertrans", Parameters = "x,y,width,height", Comment = "add a transparent layer (only for clients >= 3)", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resizepic", Parameters = "x,y,backgroundgump,width,height", Comment = "can come first if multi page. puts up some background gump", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gumppic", Parameters = "x,y,gump,hue", Comment = "put gumps in the dlg. (hue only for clients >= 3, otherwise ignored)", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gumppictiled", Parameters = "x,y,width,height,gump", Comment = "tile a gump", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tilepic", Parameters = "x,y,item", Comment = "put item tiles in the dlg.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tilepichue", Parameters = "x,y,item,hue", Comment = "put colored item tiles in the dlg.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "text", Parameters = "x,y,color,stringindex", Comment = "put some text here.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dtext", Parameters = "x,y,colour,text", Comment = "Places some text on to the page. Accepts dynamic coordinates relative to dorigin using -, +, * prefixes.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "croppedtext", Parameters = "x,y,width,height,colour,text_index", Comment = "Places some text on to the page that wraps to specified boundaries.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dcroppedtext", Parameters = "x,y,width,height,colour,text", Comment = "Places some text on to the page that wraps to specified boundaries. Accepts dynamic coordinates relative to dorigin using -, +, * prefixes.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "htmlgump", Parameters = "x,y,width,height,stringindex,hasbackground,hasscrollbar", Comment = "add an html gump that shows a text", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dhtmlgump", Parameters = "x,y,width,height,has_background,has_scrollbar,text", Comment = "Places some HTML text on to the page. Accepts dynamic coordinates relative to dorigin using -, +, * prefixes.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "xmfhtmlgump", Parameters = "x,y,width,height,clilod_id,has_background,has_scrollbar", Comment = "Places some localised HTML text on to the page.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "xmfhtmlgumpcolor", Parameters = "x,y,width,height,cliloc_id,has_background,has_scrollbar,colour", Comment = "Places some localised HTML text on to the page with the specified colour.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "xmfhtmltok", Parameters = "x,y,width,height,has_background,has_scrollbar,colour,cliloc_id,@args@", Comment = "Places some localised HTML text on to the page, with arguments to the cliloc (multiple arguments separated by @).", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "button", Parameters = "x,y,gump_down,gump_up,is_pressable,page,id", Comment = "Places a button on to the page", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "buttontileart", Parameters = "x,y,gump_down,gump_up,is_pressable,page,id,tile_id,tile_hue,tile_x,tile_y", Comment = "Places a button on to the page, with an item image placed over the top as part of the button.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "textentry", Parameters = "x,y,width,height,colour,id,text_index", Comment = "Places a text entry field on to the page where the client can enter text.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "textentrylimited", Parameters = "x,y,width,height,colour,id,text_index,limit", Comment = "Places a text entry field on to the page where the client can enter a limited amount of text.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dtextentrylimited", Parameters = "x,y,width,height,colour,id,limit,text", Comment = "Places a text entry field on to the page where the client can enter a limited amount of text. Accepts dynamic coordinates relative to dorigin using -, +, * prefixes.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tooltip", Parameters = "clilocid", Comment = "Popup a tooltip over a gump object (only for clients >= 4)", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "group", Parameters = "id", Comment = "Defines a new group ID, for grouping sets of radio buttons.", Properties = new List<PropertyTypes>() { PropertyTypes.Region, PropertyTypes.Spell, PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "radio", Parameters = "x,y,gump_check,gump_uncheck,initial_state,id", Comment = "Places a radio button on to the page.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "checkbox", Parameters = "x,y,gump_check,gump_uncheck,initial_state,id", Comment = "Places a checkbox on to the page.", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "anim", Parameters = "", Comment = "Gets or sets a mask of animations that the Mobile supports.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "armor", Parameters = "", Comment = "Gets or sets the character's base defense without armour.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "aversions", Parameters = "", Comment = "Gets or sets a list of things that the Mobile does not like.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "baseid", Parameters = "", Comment = "Gets the defname of the Mobile if set, otherwise the ID.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "bloodcolor", Parameters = "", Comment = "Gets or sets the character's blood colour (a value of -1 prevents the creature from bleeding at all.)", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "can", Parameters = "", Comment = "Gets or sets attributes for the character. See can_flags in sphere_defs.scp.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "color", Parameters = "", Comment = "Gets or sets the Mobile colour.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dam", Parameters = "min,max", Comment = "Gets or sets the base damage that the Mobile will deal without a weapon.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "defname", Parameters = "", Comment = "Gets or sets defname of the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Region, PropertyTypes.Spell, PropertyTypes.Spawn } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "desires", Parameters = "", Comment = "Gets or sets a list of things that the Mobile likes.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dex", Parameters = "", Comment = "Gets or sets the maximum dexterity value allowed for Mobiles with the class.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dispid", Parameters = "", Comment = "Gets the ID that the Mobile displays as.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "foodtype", Parameters = "", Comment = "Gets or sets a list of things that the Mobile can eat.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "height", Parameters = "", Comment = "Gets or sets the height of the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "hiredaywage", Parameters = "", Comment = "Gets or sets how much gold is needed to hire the Mobile for one day.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "icon", Parameters = "", Comment = "Gets or sets the item that can be used to represent the Mobile in figurine form.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "id", Parameters = "", Comment = "Gets or sets the ID of the Mobile to inherit property values from.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Spawn } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "instances", Parameters = "", Comment = "Returns the number of this Mobile that exist in the world.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "int", Parameters = "", Comment = "Gets or sets the maximum intelligence value allowed for Mobiles with the class.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "job", Parameters = "", Comment = "Gets the character's job title.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "maxfood", Parameters = "", Comment = "Gets the maximum food level that the Mobile can have.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "name", Parameters = "", Comment = " Gets or sets the character's name.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Skills, PropertyTypes.Region, PropertyTypes.Spell, PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "range", Parameters = "min,max", Comment = " Gets the combat range of the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "rangeh", Parameters = "", Comment = "Gets the maximum attack range of the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "rangel", Parameters = "", Comment = "Gets the minimum attack range of the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resdispdnhue", Parameters = "", Comment = "Gets or sets the colour to display as to clients who don't have a high enough RESDISP to see the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resdispnid", Parameters = "", Comment = "Gets or sets the Mobile ID to display as to clients who don't have a high enough RESDISP to see the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "reslevel", Parameters = "", Comment = "Gets or sets the minimum RESDISP required for a client to see the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resources", Parameters = "weight,resource_defname", Comment = " These are the items that you get whenever you chop up this creature's corpse.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Region, PropertyTypes.Spell, PropertyTypes.Spawn } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sound", Parameters = "sound_id,", Comment = " Plays a sound from this character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "str", Parameters = "", Comment = " Gets or sets the character's total strength.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tag", Parameters = "", Comment = "Gets or sets the value of a TAG.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Region, PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tevents", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tspeech", Parameters = "speech_defname", Comment = "Gets a list of speech handlers for the character, or adds a speech handler to the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Multi } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "argchk", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "argchkid", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "argn1", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "argtxt", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dtextentry", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Dialog } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dupeitem", Parameters = "", Comment = "It reiterates the DUPEITEM and sends the server looking to item for more information", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dupelist", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dye", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "flip", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isarmor", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isweapon", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "layer", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "repair", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "replicate", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resmake", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "reqstr", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skill", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillmake", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "speed", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tdata1", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tdata2", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tdata3", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tdata4", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tflags", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "twohands", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "type", Parameters = "", Comment = "If you want to check me on this, look in spheredefs.scp", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "value", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "weight", Parameters = "", Comment = "Sets the weight of the last resource added to the region type.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "basecomponent", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Multi } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "component", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Multi } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "multiregion", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Multi } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "regionflags", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Multi } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "shipspeed", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Multi } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "amount", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "reap", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "reapamount", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "regen", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "calcmemberindex", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Region, PropertyTypes.Spawn } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "announce", Parameters = "", Comment = "Gets or sets whether or not there will be an announcement when someone enters or exits the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "arena", Parameters = "", Comment = "Gets or sets whether or not the region is considered to be an arena.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "buildable", Parameters = "", Comment = "Gets or sets whether or not players can place buildings and ships in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "events", Parameters = "event_defname", Comment = "Gets a list of events attached to the object, or adds or removes an event to or from the object.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "flags", Parameters = "", Comment = "Gets or sets the region's attributes.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Region, PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gate", Parameters = "", Comment = "Gets or sets whether or not casting the gate travel spell is allowed in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "guarded", Parameters = "", Comment = "Gets or sets whether or not guards can be called within the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "magic", Parameters = "", Comment = "Gets or sets whether or not there is an anti-magic field in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "mark", Parameters = "", Comment = "Gets or sets whether or not casting the mark spell is allowed in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nobuild", Parameters = "", Comment = "Gets or sets whether or not players can place buildings in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nodecay", Parameters = "", Comment = "Gets or sets whether or not items will decay in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nopvp", Parameters = "", Comment = "Gets or sets whether or not PvP combat is allowed in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "p", Parameters = "", Comment = "Gets or sets the location of the region (used when using the GO command).", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "recall", Parameters = "", Comment = "Gets or sets whether or not casting the recall spell is allowed in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "recallin", Parameters = "", Comment = "Gets or sets whether or not it is possible to use the recall spell to enter the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "recallout", Parameters = "", Comment = "Gets or sets whether players can recall out of the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "rect", Parameters = "left,top,right,bottom,map", Comment = "Adds a rectangle to the region definition.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "safe", Parameters = "", Comment = "Gets or sets whether or not the region is a safe zone.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "underground", Parameters = "", Comment = "Gets or sets whether or not the region is considered to be underground.", Properties = new List<PropertyTypes>() { PropertyTypes.Region } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skill_key", Parameters = "", Comment = "Gets or sets the maximum amount of skill_key is allowed for Mobiles with the class.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillsum", Parameters = "", Comment = "Gets or sets the maximum skill total allowed for Mobiles with the class.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "statsum", Parameters = "", Comment = "Gets or sets the maximum stat total allowed for Mobiles with the class. (dexterity, intelligence and strength)", Properties = new List<PropertyTypes>() { PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "makeitem", Parameters = "item_baseid", Comment = "Checks if the client meets the criteria for crafting the item (SKILLREQ and RESOURCES (Properties) on the item's ITEMDEF).", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillmenu", Parameters = "skillmenu_id", Comment = "Searches the skillmenu to see what options are available to the client. If there are no visible items in the submenu then the option will be hidden from the client.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "test", Parameters = "resource_or_skill_list", Comment = "Checks if the client possesses all of the listed resources/skills. If they do not then the menu option will not be available to them.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "testif", Parameters = "condition", Comment = "Checks the condition. If it evaluates to false then the menu option will not be available to the client.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "cast_time", Parameters = "", Comment = "Gets or sets the length of time it takes to cast the spell, in tenths of a second. Accepts multiple values to adjust based on skill level.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "duration", Parameters = "", Comment = "Gets or sets the duration of the spell's affect in tenths of a second, if applicable. Accepts multiple values to adjust based on skill level.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "effect", Parameters = "type,item_id,speed,loop,explode,colour,rendermode", Comment = "Gets or sets a value which effects skills in different ways. (Crafting = Resource Loss % on Fail, Healing = Amount Healed). Accepts multiple values to adjust based on skill level.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "effect_id", Parameters = "", Comment = "Gets or sets the ID of the spell's visual effect.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "interrupt", Parameters = "", Comment = "Gets or sets the chance of a Mobile being interrupted when hit in combat while casting the spell. Accepts multiple values to adjust based on skill level.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "manause", Parameters = "", Comment = "Gets or sets the number of mana points needed to cast the spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "prompt_msg", Parameters = "", Comment = "Gets or sets the message shown when a Mobile casts the spell, and also forces Mobiles to select a target when non-empty.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "rune_item", Parameters = "", Comment = "Gets or sets the BASEID of the item that should be equipped when a spell has a duration.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "runes", Parameters = "", Comment = "Gets or sets the spell's words of power.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "scroll_item", Parameters = "", Comment = "Gets or sets the BASEID of the scroll that casts the spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillreq", Parameters = "", Comment = "Gets or sets a list of skills that are needed to cast the spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "char", Parameters = "", Comment = "Gets the nth Mobile attached to the account. (zero-based)", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "account", Parameters = "", Comment = "Gets the name of the account.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "block", Parameters = "", Comment = "Gets or sets whether or the not the account is blocked.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "chars", Parameters = "", Comment = "Gets how many Mobiles are attached to the account.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "chatname", Parameters = "", Comment = "Gets the name the account uses in chat.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "firstconnectdate", Parameters = "", Comment = "Gets the date and time on which the account first connected.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "firstip", Parameters = "", Comment = "Gets the IP address that the account first connected with.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "guest", Parameters = "", Comment = "Gets whether or not the account is a guest account.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "jail", Parameters = "", Comment = "Gets whether or not the account is jailed.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "lang", Parameters = "", Comment = "Gets or sets the account's language.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "lastcharuid", Parameters = "", Comment = "Gets the UID of the last Mobile used on the account.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "lastconnectdate", Parameters = "", Comment = "Gets the date and time on which the account most recently connected.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "lastip", Parameters = "", Comment = "Gets the IP address that the account most recently connected with.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "maxchars", Parameters = "", Comment = "Gets or sets the maximum number of Mobiles that the player can create on the account.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "newpassword", Parameters = "", Comment = "Gets or sets a new password that will be set on the account.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "password", Parameters = "", Comment = "Gets or sets the current password for the account.", Properties = new List<PropertyTypes>() { PropertyTypes.Account, PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "plevel", Parameters = "", Comment = "Gets or sets the account's privelege level.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "priv", Parameters = "", Comment = "Gets or sets account flags.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resdisp", Parameters = "", Comment = "Gets or sets the account's expansion level.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tagcount", Parameters = "", Comment = " Gets the number of TAGs stored on the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "totalconnecttime", Parameters = "", Comment = "Gets the total number of minutes that the account has been connected for.", Properties = new List<PropertyTypes>() { PropertyTypes.Account } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "act", Parameters = "", Comment = "Gets or sets the Mobile or item that is related to the action the Mobile is performing.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "findcont", Parameters = "", Comment = "Gets the nth item equipped to the character. (zero-based)", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "findid", Parameters = "", Comment = "Gets the first item found equipped to the Mobile or inside their backpack, with the matching BASEID.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "findlayer", Parameters = "", Comment = "Gets the item that the Mobile has equipped in a specified layer.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "findtype", Parameters = "", Comment = "Gets the first item found equipped to the Mobile or inside their backpack, with the matching TYPE.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "memoryfindtype", Parameters = "", Comment = "Gets a memory item with the specified flags.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "memoryfind", Parameters = "", Comment = "Gets a memory item that is linked to the given object.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "owner", Parameters = "", Comment = "Gets the Mobile that owns this character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "region", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "room", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sector", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "spawnitem", Parameters = "", Comment = "Gets the spawn item (t_spawn_char) that this Mobile originated from.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "topobj", Parameters = "", Comment = "Gets the top-most Mobile or item in the world that contains the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "typedef", Parameters = "", Comment = "Gets the CHARDEF that defines the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "weapon", Parameters = "", Comment = "Gets the weapon that the Mobile currently has equipped.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "ac", Parameters = "", Comment = "Returns the character's total defense.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "actarg1", Parameters = "", Comment = "Gets or sets the character's ACTARG1 value.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "actarg2", Parameters = "", Comment = "Gets or sets the character's ACTARG2 value.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "actarg3", Parameters = "", Comment = "Gets or sets the character's ACTARG3 value.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "actdiff", Parameters = "", Comment = "Gets or sets the difficulty of the character's current action.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "action", Parameters = "", Comment = "Gets or sets the skill that the Mobile is currently using.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "actp", Parameters = "", Comment = "Gets or sets the character's ACTP value.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "actprv", Parameters = "", Comment = "Gets or sets the character's ACTPRV value.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "afk", Parameters = "", Comment = "Gets or sets whether or not the Mobile is in AFK mode.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "age", Parameters = "", Comment = "Returns the age of the Mobile since its creation, in seconds.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "allskills", Parameters = "amount", Comment = "Sets all of the character's skills to the specified amount.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "attacker", Parameters = "", Comment = "Gets the number of opponents who have damaged the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "bank", Parameters = "layer", Comment = "Opens the character's bank (or the container at the specified layer) for SRC to view.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "bankbalance", Parameters = "", Comment = "Returns the total amount of gold in the character's bankbox.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "bark", Parameters = "sound_id", Comment = "Plays the specified sound (or the character's generic sound if not specified) to nearby clients from this character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "body", Parameters = "", Comment = "Gets or sets the character's body.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "bounce", Parameters = "item_uid", Comment = "Places a specified item in the character's backpack.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "bow", Parameters = "", Comment = "Makes the Mobile bow to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "cancast", Parameters = "spell_id,check_antimagic", Comment = "Returns 1 if the Mobile can cast a given spell, bypassing anti-magic field tests if check_antimagic set to 0.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "canmake", Parameters = "item_id", Comment = "Returns 1 if the Mobile has the skills and resources to craft a certain item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "canmakeskill", Parameters = "item_id", Comment = "Returns 1 if the Mobile has the skills to craft a certain item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "canmove", Parameters = "direction", Comment = "Returns 1 if the Mobile can move in the given direction.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "cansee", Parameters = "", Comment = "Returns 1 if SRC can see the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "canseelos", Parameters = "", Comment = "Returns 1 if SRC has line of sight to the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "canseelosflag", Parameters = "flag", Comment = "Returns 1 if SRC has line of sight to the character, with flags to modify what tests take place.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "consume", Parameters = "resource_list", Comment = "Removes specified resources from SRC's backpack.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "count", Parameters = "", Comment = "Returns the number of items equipped to the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "create", Parameters = "", Comment = "Gets or sets the character's age since creation, in seconds.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "criminal", Parameters = "", Comment = "Sets whether or not the Mobile is a criminal.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "damage", Parameters = "amount,type,source", Comment = "Inflicts damage upon the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dialog", Parameters = "dialog_id,page,parameters", Comment = "Displays a dialog to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dialogclose", Parameters = "dialog_id,button", Comment = "Closes a dialog that SRC has open, simulating a button press.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dialoglist", Parameters = "<3p>", Comment = "<3p>", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dir", Parameters = "", Comment = "Gets or setes the direction that the Mobile is facing.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "disconnect", Parameters = "", Comment = "Disconnects the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dismount", Parameters = "", Comment = "Dismounts the Mobile from their ride.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dispiddec", Parameters = "", Comment = "Gets the ID of the Mobile as a decimal number.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "distance", Parameters = "point_or_uid", Comment = "Gets the distance between this object and either SRC, a map location or another object.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dclick", Parameters = "object_uid", Comment = "Double clicks an object, with the Mobile as SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "drawmap", Parameters = "radius", Comment = "Starts the cartography skill, drawing a map of the local area up to radius tiles.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "drop", Parameters = "item_uid", Comment = "Drops a specified item at the character's feet.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dupe", Parameters = "", Comment = "Creates a clone of the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "edit", Parameters = "", Comment = "Displays an editing dialog for the Mobile to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "emote", Parameters = "message", Comment = "Displays a *You see* message to all nearby clients.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "emoteact", Parameters = "", Comment = "Gets, sets or toggles whether or not the Mobile will emote all of its actions.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "equip", Parameters = "item_uid", Comment = "Equips an item to the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "equiparmor", Parameters = "", Comment = "Equips the Mobile with the best armour in their backpack.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "equiphalo", Parameters = "timeout", Comment = "Equips a halo light to the character, lasting for timeout tenths of a second.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "equipweapon", Parameters = "", Comment = "Equips the Mobile with the best weapon in their backpack.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "exp", Parameters = "", Comment = "Gets or sets the character's experience points.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "face", Parameters = "object_uid", Comment = "Turns the Mobile to face a specified object or SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "fame", Parameters = "", Comment = "Gets or sets the character's fame.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "fcount", Parameters = "", Comment = "Returns the total number of items equipped to the character, including subitems", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "fix", Parameters = "", Comment = "Re-aligns the character's Z level to ground level.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "fixweight", Parameters = "", Comment = "Recalculates the character's total weight.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "font", Parameters = "", Comment = "Gets or sets the character's speech font.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "food", Parameters = "", Comment = "Gets or sets the character's food level.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "forgive", Parameters = "", Comment = "Revokes the character's jailed status.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "go", Parameters = "location", Comment = "Teleports the Mobile to the specified location.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gochar", Parameters = "n", Comment = "Teleports the Mobile to the nth Mobile in the world.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gocharid", Parameters = "character_defname", Comment = "Teleports the Mobile to the next characer in the world with the specified BASEID", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gocli", Parameters = "", Comment = "eleports the Mobile to the nth online player. (zero-based)", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "goitemid", Parameters = "item_defname", Comment = "Teleports the Mobile to the next item in the world with the specified BASEID.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gold", Parameters = "", Comment = "Gets or sets the amount of gold the Mobile has.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "goname", Parameters = "name", Comment = "Teleports the Mobile to the next Mobile or item in the world with the specified name, accepts wildcards (*).", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gosock", Parameters = "socket", Comment = "Teleports the Mobile to the online player with the specified socket number.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gotype", Parameters = "item_type", Comment = "Teleports the Mobile to the next item in the world with the specified TYPE.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gouid", Parameters = "object_uid", Comment = "Teleports the Mobile to the object with the specified UID.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "guildabbrev", Parameters = "", Comment = "Returns the character's guild abbreviation.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "hear", Parameters = "text", Comment = "For NPCs, acts as if SRC had spoken the specified text. For players, displays text as a system message.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "hits", Parameters = "", Comment = "Gets or sets the character's hitpoints.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "home", Parameters = "", Comment = "Gets or sets the character's home location.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "hungry", Parameters = "", Comment = "Displays this character's hunger level to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "invis", Parameters = "", Comment = "Sets whether or not the Mobile is invisible.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "invul", Parameters = "", Comment = "Sets whether or not the Mobile is invulnerable.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "ischar", Parameters = "", Comment = "Returns 1 if the object is a character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "iscont", Parameters = "", Comment = "Returns 1 if the object is a container.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isdialogopen", Parameters = "dialog_id", Comment = "Returns 1 if SRC has the specified dialog visible on their screen.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isevent", Parameters = "", Comment = "Returns 1 if the object has an event attached to it.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isitem", Parameters = "", Comment = "Returns 1 if the object is an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isneartype", Parameters = "type,distance,flags", Comment = "Returns 1 if a nearby item has the given TYPE.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isneartypetop", Parameters = "type,distance,flags", Comment = "Returns a nearby world location of a nearby item which has the given TYPE.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isplayer", Parameters = "", Comment = "Returns 1 if the object is a player.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "istevent", Parameters = "", Comment = "Returns 1 if the object has an event attached to its CHARDEF.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "info", Parameters = "", Comment = "Displays an information dialog about the Mobile to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isgm", Parameters = "", Comment = "Returns 1 if the Mobile is in GM mode.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isinparty", Parameters = "", Comment = "Returns 1 if the Mobile is in a party.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "ismypet", Parameters = "", Comment = "Returns 1 if the Mobile belongs to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isonline", Parameters = "", Comment = "Returns 1 if the Mobile is considered to be online.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isstuck", Parameters = "", Comment = "Returns 1 if the Mobile cannot walk in any direction.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isvendor", Parameters = "", Comment = "Returns 1 if the Mobile is a vendor.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isverticalspace", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "karma", Parameters = "", Comment = " Gets or sets the character's karma.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "kill", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "level", Parameters = "", Comment = " Gets or sets the character's experience level.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "light", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "mana", Parameters = "", Comment = " Gets or sets the character's mana.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "map", Parameters = "", Comment = " Gets or sets the map that this object is located.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "maxhits", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "maxmana", Parameters = "", Comment = " Gets or sets the character's maximum mana.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "maxstam", Parameters = "", Comment = " Gets or sets the character's maximum stamina.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "maxweight", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "memory", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "menu", Parameters = "menu_defname", Comment = " Displays a menu to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "message", Parameters = "@colornum,message", Comment = " Displays a message above this Mobile to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sysmessage", Parameters = "@colornum,message", Comment = " Displays a message above this Mobile to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "messageua", Parameters = "@colornum,message", Comment = " Displays a UNICODE message above this Mobile to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "modar", Parameters = "", Comment = " Gets or sets a modifier for the character's armour rating.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "moddex", Parameters = "", Comment = " Gets or sets the character's dexterity modifier.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "modint", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "modmaxweight", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "modstr", Parameters = "", Comment = " Gets or sets the character's strength modifier.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "mount", Parameters = "mount_uid", Comment = " Attempts to mount the Mobile on to the specified mount.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "move", Parameters = "", Comment = " Moves the object relative to its current position.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "movenear", Parameters = "object_uid,", Comment = " Moves the Mobile to a random location near another object within a certain distance.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "moveto", Parameters = "location", Comment = " Moves the Mobile to a specific location.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "newbieskill", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "newgold", Parameters = "amount", Comment = " Generates amount gold in the character's backpack.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "newloot", Parameters = "item_or_template_defname", Comment = " Generates the specified item or template into the character's backpack, providing that they are an NPC that hasn't been summoned.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nightsight", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "notogetflag", Parameters = "viewer_uid,", Comment = " Gets the character's notoriety flags as seen by the specified viewer.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "npc", Parameters = "", Comment = " Gets or sets the character's AI type.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nudgedown", Parameters = "amount", Comment = " Decreases the character's Z level.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nudgeup", Parameters = "amount", Comment = " Increases the characer's Z level.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "obody", Parameters = "", Comment = " Gets or sets the character's original body.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "openpaperdoll", Parameters = "character_uid", Comment = " Displays a specified character's paperdoll to this character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "oskin", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "odex", Parameters = "", Comment = " Gets or sets the character's base dexterity (without modifiers).", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "oint", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "ostr", Parameters = "", Comment = " Gets or sets the character's base strength (without modifiers).", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "pack", Parameters = "", Comment = " Opens the character's backpack for SRC to view.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "poison", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "poly", Parameters = "character_id", Comment = " Begins casting the polymorph spell, with character_id being the Mobile to turn into.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "promptconsole", Parameters = "function,", Comment = " Displays a prompt message to SRC and passes their response into a specified function.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "promptconsoleu", Parameters = "function,", Comment = " Displays a prompt message to SRC and passes their response into a specified function, supporting UNICODE response.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "privset", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "release", Parameters = "", Comment = " Clears the character's owners.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "remove", Parameters = "allow_player_removal", Comment = " Deletes the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "removefromview", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "rescold", Parameters = "", Comment = " Gets or sets the character's resistance to cold.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "rescount", Parameters = "item_defname", Comment = " Returns the total amount of a specific item equipped to the Mobile or inside their baackpack.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resendtooltip", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resenergy", Parameters = "", Comment = " Gets or sets the character's resistance to energy.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resfire", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "respoison", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "restest", Parameters = "item_list", Comment = " Returns 1 if all of the items in the list can be found equipped to the Mobile or inside their backpack.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resurrect", Parameters = "force", Comment = " Resurrects the character. If force is 1 then usual anti-magic checks are bypasses.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "salute", Parameters = "object_uid", Comment = " Makes the Mobile salute a specified object or SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "say", Parameters = "@colornum,message", Comment = " Makes the Mobile speak a message.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sayu", Parameters = "@colornum,message", Comment = " Makes the Mobile speak a UTF-8 message", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sayua", Parameters = "@colornum,message", Comment = " MAkes the Mobile speak a UNICODE message.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sdialog", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "serial", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sex", Parameters = "value_male:value_female", Comment = " Returns value_male or value_female depending on the character's gender.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sextantp", Parameters = "location", Comment = " Converts the character's location or a specified location into sextant coordinates.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skill_name", Parameters = "", Comment = " Gets or sets the character's skill level in skill_name.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillcheck", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillbest", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillgain", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skilltest", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skilltotal", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillusequick", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sleep", Parameters = "fall_forwards", Comment = " Makes the Mobile appear to sleep.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "spelleffect", Parameters = "spell_id,", Comment = " Causes the Mobile to be affected by a spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "stam", Parameters = "", Comment = " Gets or sets the character's stamina.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "stone", Parameters = "", Comment = " Gets or sets whether or not the Mobile is trapped in stone.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "suicide", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "summoncage", Parameters = "", Comment = " Teleports the Mobile to SRC's, surrounded by a cage multi.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "summonto", Parameters = "", Comment = " Teleports the Mobile to SRC's position.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tagat", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "taglist", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "targetfgmw", Parameters = "function", Comment = " Displays a targeting cursor to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "timer", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "timerd", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "timerf", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "title", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "townabbrev", Parameters = "", Comment = " Returns the character's town abbreviation.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "trigger", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "uid", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "underwear", Parameters = "", Comment = " Toggles the display of underwear on the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "unequip", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "update", Parameters = "", Comment = " Updates the state of the Mobile to nearby clients.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "updatex", Parameters = "", Comment = " Updates the state of the Mobile to nearby clients, removing it from their view first to ensure a full refresh.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "useitem", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "visualrange", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "where", Parameters = "", Comment = " Describes the character's location to SRC.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "x", Parameters = "", Comment = "Gets the X coordinate of the location.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "y", Parameters = "", Comment = "Gets the Y coordinate of the location.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "z", Parameters = "", Comment = "Gets the Z position of the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "guild", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillclass", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "town", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "curfollower", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "deaths", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dspeech", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gmpage", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player, PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "isdspeech", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "kick", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "kills", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "krtoolbarstatus", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "lastused", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "luck", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "maxfollower", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "pflag", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "profile", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skilllock", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "speedmode", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "statlock", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tithing", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Player } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "actpri", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "buy", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "bye", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "flee", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "goto", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "hire", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "leave", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "homedist", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "petretrieve", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "petstable", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "restock", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "run", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sell", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "shrink", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "speech", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "speechcolor", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "train", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "vendcap", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "vendgold", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "walk", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Npc } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gmpagep", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "housedesign", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "party", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "targ", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "targp", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "targprop", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "targprv", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "add", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "addbuff", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "addcliloc", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "addcontextentry", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "allmove", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "allshow", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "arrowquest", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "badspawn", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "bankself", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "cast", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "charlist", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "clearctags", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "clientis3d", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "clientiskr", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "clientversion", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "ctag", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "ctaglist", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "debug", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "detail", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "everbtarg", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "extract", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "flush", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client, PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gm", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "gotarg", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "hearall", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "information", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "last", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "lastevent", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "link", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "midilist", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nudge", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nuke", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "nukechar", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "privshow", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "removebuff", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "reportedcliver", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "reportedcliver.full", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "resend", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "save", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "screensize", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "screensize.x", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "screensize.y", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "scroll", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "self", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sendpacket", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "set", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "showskills", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "skillselect", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "summon", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "x", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sysmessageloc", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sysmessagelocex", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "sysmessageua", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "targtxt", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tele", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "tile", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "unextract", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "version", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "weblink", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "xcommand", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Client } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "aexecute", Parameters = "function,command", Comment = "Executes an SQL command in a background thread, calling function when complete. Returns 1 if the command is successfully queued.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "aquery", Parameters = "function,command", Comment = "Executes an SQL command in a background thread, calling function when complete. Returns 1 if the command is successfully queued.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "close", Parameters = "", Comment = "Closes the connection to the database.// Closes the currently open file.", Properties = new List<PropertyTypes>() { PropertyTypes.File, PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "connect", Parameters = "", Comment = "Opens a connection to the database, using the settings from Sphere.ini.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "connected", Parameters = "", Comment = "Returns 1 if the database is connected.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "escapedata", Parameters = "text", Comment = "Returns text as an escaped SQL string.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "execute", Parameters = "command", Comment = "Exectutes an SQL command that doesn't return any result.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "query", Parameters = "command", Comment = "Executes an SQL command that returns results.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "row", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "row.numcols", Parameters = "", Comment = "Returns the number of columns returned in the last query.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "row.numrows", Parameters = "", Comment = "Returns the number of rows returned in the last query.", Properties = new List<PropertyTypes>() { PropertyTypes.Database } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "deletefile", Parameters = "file_name", Comment = "Deletes file_name.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "fileexist", Parameters = "file_name", Comment = "Returns 1 if file_name exists.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "filelines", Parameters = "file_name", Comment = "Returns the total number of lines in file_name.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "filepath", Parameters = "", Comment = "Returns the name of the currently open file.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "inuse", Parameters = "", Comment = "Returns 1 if a file is currently open.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "iseof", Parameters = "", Comment = "Returns 1 if there are no more lines left to read from the file.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "length", Parameters = "", Comment = "Returns the total length of the currently open file, in bytes.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "mode.append", Parameters = "", Comment = "Gets or sets whether or not the file will be appended to when opened. Cannot be set after the file has been opened.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "mode.create", Parameters = "", Comment = "Gets or sets whether or not the file will be created when opened. Cannot be set after the file has been opened.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "mode.readlflag", Parameters = "", Comment = "Gets or sets whether or not the file will be opened for reading from. Cannot be set after the file has been opened.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "mode.writeflag", Parameters = "", Comment = "Gets or sets whether or not the file will be opened for writing to. Cannot be set after the file has been opened.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "mode.setdefault", Parameters = "", Comment = "Sets the mode to the default setting. Cannot be set after the file has been opened.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "open", Parameters = "file_name", Comment = "Opens a file, and returns 1 if the attempt was successful, using the set MODE flags.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "position", Parameters = "", Comment = "Gets the current position in the currently open file, in bytes.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "readbyte", Parameters = "", Comment = "Reads the next byte from the currently open file.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "readchar", Parameters = "", Comment = "Reads the next Mobile from the currently open file.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "readline", Parameters = "n", Comment = "Reads the nth line from the currently open file (1-based). 0 Reads the last line.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "seek", Parameters = "position", Comment = "Changes the current position in the currently open file to position. Accepts \"BEGIN\" for the start of the file and \"END\" for the end of the file. Returns the new position in the file.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "write", Parameters = "text", Comment = "Writes text to the currently open file.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "writechr", Parameters = "ascii_value", Comment = "Writes a single Mobile to the currently open file.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "writeline", Parameters = "text", Comment = "Writes text to the currently open file, with newline character(s) on the end.", Properties = new List<PropertyTypes>() { PropertyTypes.File } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "handled", Parameters = "", Comment = "Gets the UID of the Mobile currently handling the page.", Properties = new List<PropertyTypes>() { } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "reason", Parameters = "", Comment = "Gets or sets the reason for the page.", Properties = new List<PropertyTypes>() { } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "status", Parameters = "", Comment = "Gets the status of the page sender (OFFLINE, LOGIN or Mobile name).", Properties = new List<PropertyTypes>() { } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "time", Parameters = "", Comment = "Gets the time since the page was originally sent in seconds.", Properties = new List<PropertyTypes>() { } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "cont", Parameters = "", Comment = "Gets or sets the Mobile or container item that the object is inside.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "addcircle", Parameters = "spell_id", Comment = "Gets whether or not a spell exists in the spellbook, or adds a spell to the spellbook.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "addspell", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "attr", Parameters = "", Comment = "Gets or sets the item's attribute flags.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "cleartags", Parameters = "prefix", Comment = "Removes all TAGs from the item that start with the given prefix.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "contconsume", Parameters = "resource_list", Comment = "Deletes items from inside the container.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "contgrid", Parameters = "", Comment = "If in a container, gets or sets the grid number that the item occupies (in KR's grid view)", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "contp", Parameters = "", Comment = "Gets or sets the position of the item within its container.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "decay", Parameters = "time", Comment = "Sets the decay timer for the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dmgcold", Parameters = "", Comment = "Gets or sets the amount of cold damage the weapon will give.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dmgenergy", Parameters = "", Comment = "Gets or sets the amount of energy damage the weapon will give.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dmgfire", Parameters = "", Comment = "Gets or sets the amount of fire damage the weapon will give.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "dmgpoison", Parameters = "", Comment = "Gets or sets the amount of poison damage the weapon will give.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "fruit", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "hitpoints", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "more1", Parameters = "", Comment = "Gets or sets the MORE1 value for the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "more1h", Parameters = "", Comment = "Gets or sets the upper 4 bytes of the item's MORE1 value.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "more1l", Parameters = "", Comment = "Gets or sets the lower 4 bytes of the item's MORE1 value.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "more2", Parameters = "", Comment = "Gets or sets the MORE2 value for the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "more2h", Parameters = "", Comment = "Gets or sets the upper 4 bytes of the item's MORE2 value.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "more2l", Parameters = "", Comment = "Gets or sets the lower 4 bytes of the item's MORE2 value.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "morem", Parameters = "", Comment = "Gets or sets the MOREM value for the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "morex", Parameters = "", Comment = "Gets or sets the MOREX value for the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "morey", Parameters = "", Comment = "Gets or sets the MOREY value for the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "morez", Parameters = "", Comment = "Gets or sets the MOREZ value for the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "morep", Parameters = "", Comment = "Gets or sets the MOREP value for the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "use", Parameters = "", Comment = "", Properties = new List<PropertyTypes>() { PropertyTypes.Item } });

            #endregion keywordsInformation

            #region TRIGGERS information

            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@AfterClick", Parameters = "", Comment = " Fires when the object has been single-clicked, just before the overhead name is shown.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Attack", Parameters = "", Comment = " Fires when the Mobile begins attacking another.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@CallGuards", Parameters = "", Comment = " Fires when the Mobile calls for guards.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@CharAttack", Parameters = "", Comment = " Fires when the Mobile is attacked by another character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@CharClick", Parameters = "", Comment = " Fires when the Mobile is clicked by another character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@CharClientTooltip", Parameters = "", Comment = " Fires when the tooltips are about to be sent to the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@CharDClick", Parameters = "", Comment = " Fires when the Mobile double clicks another character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@CharTradeAccepted", Parameters = "", Comment = " Fires when another Mobile accepts trade with the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Click", Parameters = "", Comment = " Fires when the object has been single-clicked.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ClientTooltip", Parameters = "", Comment = " Fires when tooltips for this Mobile are about to be sent to a client.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ContextMenuRequest", Parameters = "", Comment = " Fires when a client requests the context menu options for the object.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ContextMenuSelect", Parameters = "", Comment = " Fires when a client selects a context menu option for the object.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Create", Parameters = "", Comment = " Fires when the object is initially created, before it is placed in the world.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Criminal", Parameters = "", Comment = " Fires when the Mobile becomes a criminal.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@DClick", Parameters = "", Comment = " Fires when the object is double-clicked.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Death", Parameters = "", Comment = " Fires when the character's hitpoints reaches zero.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@DeathCorpse", Parameters = "", Comment = " Fires when a corpse is created for the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Destroy", Parameters = "", Comment = " Fires when the object is being deleted.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Dismount", Parameters = "", Comment = " Fires when the Mobile dismounts their ride.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@EnvironChange", Parameters = "", Comment = " Fires when the environment changes for the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ExpChange", Parameters = "", Comment = " Fires when the character's experience points change.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ExpLevelChange", Parameters = "", Comment = " Fires when the character's experience level changes.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@FameChange", Parameters = "", Comment = " Fires when the character's fame changes.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@GetHit", Parameters = "", Comment = " Fires when the Mobile receives damage.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Hit", Parameters = "", Comment = " Fires when the Mobile hits another in combat.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@HitMiss", Parameters = "", Comment = " Fires when the Mobile fails to hit another in combat.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@HouseDesignCommit", Parameters = "", Comment = " Fires when the Mobile commits a new house design.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@HouseDesignExit", Parameters = "", Comment = " Fires when the Mobile exits house design mode.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Hunger", Parameters = "", Comment = " Fires when the character's food level decreases.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemAfterClick", Parameters = "", Comment = " Fires when the Mobile single-clicks an item, just before the overhead name is shown.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemBuy", Parameters = "", Comment = " Fires when the Mobile buys an item from a vendor.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemClick", Parameters = "", Comment = " Fires when the Mobile single-clicks an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemClientTooltip", Parameters = "", Comment = " Fires when the tooltips are about to be sent to the client for an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemContextMenuRequest", Parameters = "", Comment = " Fires when the Mobile requests the context menu options for an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemContextMenuSelect", Parameters = "", Comment = " Fires when the Mobile selects a context menu option for an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemCreate", Parameters = "", Comment = " Fires when an item is initially created, before it is placed in the world, and the Mobile is in some way responsible for it.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemDamage", Parameters = "", Comment = " Fires when the Mobile damages an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemDClick", Parameters = "", Comment = " Fires when the Mobile double-clicks an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemDropOn_Char", Parameters = "", Comment = " Fires when the Mobile drops an item on to a character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemDropOn_Ground", Parameters = "", Comment = " Fires when the Mobile drops an item on to the ground.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemDropOn_Item", Parameters = "", Comment = " Fires when the Mobile drops an item on to another item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemDropOn_Self", Parameters = "", Comment = " Fires when the Mobile drops an item inside another item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemEquip", Parameters = "", Comment = " Fires when the Mobile equips an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemEquipTest", Parameters = "", Comment = " Fires when the characer is about to equip an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemPickUp_Ground", Parameters = "", Comment = " Fires when the Mobile picks an item up from the ground.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemPickUp_Pack", Parameters = "", Comment = " Fires when the Mobile picks an item up from inside a container.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemPickUp_Self", Parameters = "", Comment = " Fires when the Mobile picks an item up from inside another item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemPickUp_Stack", Parameters = "", Comment = " Fires when the Mobile picks up an item from a stack.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemSell", Parameters = "", Comment = " Fires when the Mobile sells an item to a vendor.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemSpellEffect", Parameters = "", Comment = " Fires when the Mobile hits an item with a spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemStep", Parameters = "", Comment = " Fires when the Mobile steps on an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemTargOn_Cancel", Parameters = "", Comment = " Fires when the Mobile cancels an item's target cursor.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemTargOn_Char", Parameters = "", Comment = " Fires when the Mobile targets a Mobile with an item's target cursor.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemTargOn_Ground", Parameters = "", Comment = " Fires when the Mobile targets the ground with an item's target cursor.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemTargOn_Item", Parameters = "", Comment = " Fires when the Mobile targets an item with an item's target cursor.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemToolTip", Parameters = "", Comment = " Fires when the Mobile requests old-style tooltips for an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ItemUnEquip", Parameters = "", Comment = " Fires when the Mobile unequips an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Jailed", Parameters = "", Comment = " Fires when the Mobile is sent to jail.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@KarmaChange", Parameters = "", Comment = " Fires when the character's karma changes.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Kill", Parameters = "", Comment = " Fires when the Mobile kills another character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Login", Parameters = "", Comment = " Fires when the Mobile logs in.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Logout", Parameters = "", Comment = " Fires when the Mobile logs out.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Mount", Parameters = "", Comment = " Fires when the Mobile mounts a ride.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@MurderDecay", Parameters = "", Comment = " Fires when one of the character's kills is about to decay.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@MurderMark", Parameters = "", Comment = " Fires when the Mobile is about to gain a kill.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCAcceptItem", Parameters = "", Comment = " Fires when the NPC receives an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCActFight", Parameters = "", Comment = " Fires when the NPC makes a combat decision.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCActFollow", Parameters = "", Comment = " Fires when the NPC follows another character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCAction", Parameters = "", Comment = " Fires when the NPC is about to perform an AI action.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCHearGreeting", Parameters = "", Comment = " Fires when the NPC hears a Mobile for the first time.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCHearUnknown", Parameters = "", Comment = " Fires when the NPC hears something they don't understand.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCLookAtChar", Parameters = "", Comment = " Fires then the NPC looks at a character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCLookAtItem", Parameters = "", Comment = " Fires when the NPC looks at an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCLostTeleport", Parameters = "", Comment = " Fires when the NPC is lost and is about to be teleported back to their HOME.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCRefuseItem", Parameters = "", Comment = " Fires when the NPC refuses an item being given to them.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCRestock", Parameters = "", Comment = " Fires when the NPC is having their items restocked.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCSeeNewPlayer", Parameters = "", Comment = " Fires when the NPC first sees a player.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCSeeWantItem", Parameters = "", Comment = " Fires when the NPC sees an item they want.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@NPCSpecialAction", Parameters = "", Comment = " Fires when the NPC is about to perform a special action (leaving fire trail, dropping web).", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@PersonalSpace", Parameters = "", Comment = " Fires when the Mobile is stepped on.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@PetDesert", Parameters = "", Comment = " Fires when the Mobile deserts its owner.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Profile", Parameters = "", Comment = " Fires when a player attempts to read the character's profile from the paperdoll.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ReceiveItem", Parameters = "", Comment = " Fires when the NPC receives an item from another character, before they decide if they want it or not.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@RegionEnter", Parameters = "", Comment = " Fires when the Mobile enters a region.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@RegionLeave", Parameters = "", Comment = " Fires when the Mobile leaves a region.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Rename", Parameters = "", Comment = " Fires when the Mobile renames another.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SeeCrime", Parameters = "", Comment = " Fires when the Mobile sees a crime take place.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillAbort", Parameters = "", Comment = " Fires when the Mobile aborts a skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillChange", Parameters = "", Comment = " Fires when the character's skill level changes.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillFail", Parameters = "", Comment = " Fires when the Mobile fails a skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillGain", Parameters = "", Comment = " Fires when the Mobile has a chance to gain in a skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillMakeItem", Parameters = "", Comment = " Fires when the Mobile crafts an item.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillMenu", Parameters = "", Comment = " Fires when a skill menu is shown to the character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillPreStart", Parameters = "", Comment = " Fires when the Mobile starts a skill, before any hardcoded action takes place.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillSelect", Parameters = "", Comment = " Fires when the Mobile selects a skill on their skill menu.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillStart", Parameters = "", Comment = " Fires when the Mobile starts a skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillSuccess", Parameters = "", Comment = " Fires when the Mobile succeeds at a skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SkillUseQuick", Parameters = "", Comment = " Fires when the Mobile quickly uses a skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SpellBook", Parameters = "", Comment = " Fires when the Mobile opens their spellbook.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SpellCast", Parameters = "", Comment = " Fires when the Mobile casts a spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SpellEffect", Parameters = "", Comment = " Fires when the Mobile is hit by the effects of a spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SpellFail", Parameters = "", Comment = " Fires when the Mobile fails to cast a spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SpellSelect", Parameters = "", Comment = " Fires when the Mobile selects a spell to cast.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@SpellSuccess", Parameters = "", Comment = " Fires when the Mobile successfully casts a spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@StatChange", Parameters = "", Comment = " Fires when the character's STR, DEX or INT is changed through skill gain.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@StepStealth", Parameters = "", Comment = " Fires when the Mobile takes a step whilst hidden.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ToolTip", Parameters = "", Comment = " Fires when a player requests old-style tooltips for this character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@TradeAccepted", Parameters = "", Comment = " Fires when the Mobile accepts a trade with another player.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserBugReport", Parameters = "", Comment = " Fires when the player submits a bug report.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserChatButton", Parameters = "", Comment = " Fires when the player presses the Chat button on the paperdoll.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserExtCmd", Parameters = "", Comment = " Fires when the player sends an extended command packet. (used by some macros)", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserExWalkLimit", Parameters = "", Comment = " Fires when the player's movement is restricted by the movement speed settings in Sphere.ini", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserGuildButton", Parameters = "", Comment = " Fires when the player presses the Guild button on the paperdoll.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserKRToolbar", Parameters = "", Comment = " Fires when the player presses a button on the toolbar.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserMailBag", Parameters = "", Comment = " Fires when the player drags the mail bag on to another character.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserQuestArrowClick", Parameters = "", Comment = " Fires when the player clicks the quest arrow.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserQuestButton", Parameters = "", Comment = " Fires when the player presses the Quest button on the paperdoll.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserSkills", Parameters = "", Comment = " Fires when the player opens their skill menu, or a skill update is sent to the player.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserSpecialMove", Parameters = "", Comment = " Fires when the player uses a special move.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserStats", Parameters = "", Comment = " Fires when the player opens the status window.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserVirtue", Parameters = "", Comment = " Fires when the player presses on the Virtue button.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserVirtueInvoke", Parameters = "", Comment = " Fires when the player invokes a virtue through macros.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UserWarmode", Parameters = "", Comment = " Fires when the player switches between war and peace mode.", Properties = new List<PropertyTypes>() { PropertyTypes.Mobile, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Buy", Parameters = "", Comment = " Fires when the item is being bought from a vendor.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Damage", Parameters = "", Comment = " Fires when the item receives damage.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@DropOn_Char", Parameters = "", Comment = " Fires when the item has been dropped on to a character.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@DropOn_Ground", Parameters = "", Comment = " Fires when the item has been dropped on to the ground.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@DropOn_Item", Parameters = "", Comment = " Fires when the item is dropped on to another item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@DropOn_Self", Parameters = "", Comment = " Fires when an item has been dropped on to this item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Equip", Parameters = "", Comment = " Fires when the item has been equipped.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@EquipTest", Parameters = "", Comment = " Fires when the item is about to be equipped.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@PickUp_Ground", Parameters = "", Comment = " Fires when the item ihas been picked up from the ground.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@PickUp_Pack", Parameters = "", Comment = " Fires when the item is picked up from inside a container.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@PickUp_Self", Parameters = "", Comment = " Fires when an item has been picked up from inside the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@PickUp_Stack", Parameters = "", Comment = " Fires when the item is picked up from a stack.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Sell", Parameters = "", Comment = " Fires when the item is sold to a vendor.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Step", Parameters = "", Comment = " Fires when a Mobile steps on the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Region, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@TargOn_Cancel", Parameters = "", Comment = " Fires when a target is cancelled from the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@TargOn_Char", Parameters = "", Comment = " Fires when a Mobile is targeted from the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@TargOn_Ground", Parameters = "", Comment = " Fires when the ground is targeted from the item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@TargOn_Item", Parameters = "", Comment = " Fires when an item is targeted from this item.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Timer", Parameters = "", Comment = " Fires when the item's timer expires.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UnEquip", Parameters = "", Comment = " Fires when the item is unequipped.", Properties = new List<PropertyTypes>() { PropertyTypes.Item, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ResourceTest", Parameters = "", Comment = " Fires when the resource is being considered for spawning.", Properties = new List<PropertyTypes>() { PropertyTypes.Region, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@ResourceFound", Parameters = "", Comment = " Fires when the resource is spawned.", Properties = new List<PropertyTypes>() { PropertyTypes.Region, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@CliPeriodic", Parameters = "", Comment = " Fires multiple times approximately every 30 seconds, for each client in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Enter", Parameters = "", Comment = " Fires when a Mobile enters the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Exit", Parameters = "", Comment = " Fires when a Mobile exits the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@RegPeriodic", Parameters = "", Comment = " Fires once approximately every 30 seconds, as long as there is at least one client in the region.", Properties = new List<PropertyTypes>() { PropertyTypes.Region, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Abort", Parameters = "", Comment = " Fires when a Mobile aborts an attempt at using the skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Fail", Parameters = "", Comment = " Fires when a Mobile fails an attempt at using the skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Spell, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Gain", Parameters = "", Comment = " Fires when a Mobile is given the chance to gain in the skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@PreStart", Parameters = "", Comment = " Fires when a Mobile starts to use the skill, before any hardcoded behaviour takes place.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Select", Parameters = "", Comment = " Fires when a Mobile selects the skill from their skill menu.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Spell, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Start", Parameters = "", Comment = " Fires when a Mobile starts to use the skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Spell, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Success", Parameters = "", Comment = " Fires when a Mobile succeeds an attempt at using the skill.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Spell, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@UseQuick", Parameters = "", Comment = " Fires when a Mobile quickly uses the skill, without changing their ACTION.", Properties = new List<PropertyTypes>() { PropertyTypes.Skills, PropertyTypes.Trigger } });
            KeywordsInformation.Add(new PopupToolTip() { Name = "On=@Effect", Parameters = "", Comment = " Fires when a Mobile or item is hit by the spell.", Properties = new List<PropertyTypes>() { PropertyTypes.Spell, PropertyTypes.Trigger } });

            #endregion TRIGGERS information
            
            PopupToolTip[] test = KeywordsInformation.FindAll(p => p.Name.IndexOf("@") == -1).ToArray();
            keywords = new MethodAuto[test.Length];
            for (int i = 0; i < test.Length; i++)
            {
                keywords[i] = new MethodAuto(test[i]);
                keywords[i].ImageIndex = 2;
            }

            #region KeywordsAutoComplete
            for (int i = 0; i < methods.Length; i++)
                methods[i].loadPopupToolTip(KeywordsInformation.Find(p => p.Name == methods[i].Text));

            #endregion KeywordsAutoComplete
        }
    }
}