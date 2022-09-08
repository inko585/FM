namespace AE.Graphics.Wpf.FontAwesome
{
    public static class FontAwesomeUtils
    {
        /*
         * You can find the font #name directly in *.otf file (open it with notepad)
         */
        public const string FONT_AWESOME_SOLID_FILE_NAME = @"/Assets/fa5_solid_900.otf";
        public const string FONT_AWESOME_SOLID_NAME = @"#Font Awesome 5 Free";
        public const string FONT_AWESOME_REGULAR_FILE_NAME = @"/Assets/fa5_regular_400.otf";
        public const string FONT_AWESOME_REGULAR_NAME = "#Font Awesome 5 Free";
        public const string FONT_AWESOME_BRANDS_REGULAR_FILE_NAME = @"/Assets/fa5_brands_regular_400.otf";
        public const string FONT_AWESOME_BRANDS_REGULAR_NAME = @"#Font Awesome 5 Brands";

        public const string FONT_AWESOME_SOLID = FONT_AWESOME_SOLID_FILE_NAME + FONT_AWESOME_SOLID_NAME;
        public const string FONT_AWESOME_REGULAR = FONT_AWESOME_REGULAR_FILE_NAME + FONT_AWESOME_REGULAR_NAME;
        public const string FONT_AWESOME_BRANDS_REGULAR = FONT_AWESOME_BRANDS_REGULAR_FILE_NAME + FONT_AWESOME_BRANDS_REGULAR_NAME;

        public static string GetFontFamilyForPackageType(FontAwesomeIconPackageType fontAwesomePackageType)
        {
            return GetFontFamilyForIconPackageType(fontAwesomePackageType, false);
        }

        public static string GetFontFamilyForIconPackageType(FontAwesomeIconPackageType fontAwesomePackageType, bool onlyFileName)
        {
            string fontAwesomePackage = "";
            switch (fontAwesomePackageType)
            {
                case FontAwesomeIconPackageType.SOLID:
                    fontAwesomePackage = onlyFileName ? FONT_AWESOME_SOLID_FILE_NAME : FONT_AWESOME_SOLID;
                    break;

                case FontAwesomeIconPackageType.REGULAR:
                    fontAwesomePackage = onlyFileName ? FONT_AWESOME_REGULAR_FILE_NAME : FONT_AWESOME_REGULAR;
                    break;

                case FontAwesomeIconPackageType.BRAND_REGULAR:
                    fontAwesomePackage = onlyFileName ? FONT_AWESOME_BRANDS_REGULAR_FILE_NAME : FONT_AWESOME_BRANDS_REGULAR;
                    break;

                default:
                    fontAwesomePackage = onlyFileName ? FONT_AWESOME_SOLID_FILE_NAME : FONT_AWESOME_SOLID;
                    break;
            }
            return fontAwesomePackage;
        }

        public static string GetUnicodeByIconType(FontAwesomeIconType fontAwesomeIconType)
        {
            string unicode = "";
            switch (fontAwesomeIconType)
            {
                case FontAwesomeIconType.SOLID_AD:
                    unicode = FontAwesomeUnicode.SOLID_AD;
                    break;

                case FontAwesomeIconType.SOLID_ADDRESS_BOOK:
                    unicode = FontAwesomeUnicode.SOLID_ADDRESS_BOOK;
                    break;

                case FontAwesomeIconType.SOLID_ADDRESS_CARD:
                    unicode = FontAwesomeUnicode.SOLID_ADDRESS_CARD;
                    break;

                case FontAwesomeIconType.SOLID_ADJUST:
                    unicode = FontAwesomeUnicode.SOLID_ADJUST;
                    break;

                case FontAwesomeIconType.SOLID_AIR_FRESHENER:
                    unicode = FontAwesomeUnicode.SOLID_AIR_FRESHENER;
                    break;

                case FontAwesomeIconType.SOLID_ALIGN_CENTER:
                    unicode = FontAwesomeUnicode.SOLID_ALIGN_CENTER;
                    break;

                case FontAwesomeIconType.SOLID_ALIGN_JUSTIFY:
                    unicode = FontAwesomeUnicode.SOLID_ALIGN_JUSTIFY;
                    break;

                case FontAwesomeIconType.SOLID_ALIGN_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_ALIGN_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_ALIGN_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_ALIGN_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_ALLERGIES:
                    unicode = FontAwesomeUnicode.SOLID_ALLERGIES;
                    break;

                case FontAwesomeIconType.SOLID_AMBULANCE:
                    unicode = FontAwesomeUnicode.SOLID_AMBULANCE;
                    break;

                case FontAwesomeIconType.SOLID_AMERICAN_SIGN_LANGUAGE_INTERPRETING:
                    unicode = FontAwesomeUnicode.SOLID_AMERICAN_SIGN_LANGUAGE_INTERPRETING;
                    break;

                case FontAwesomeIconType.SOLID_ANCHOR:
                    unicode = FontAwesomeUnicode.SOLID_ANCHOR;
                    break;

                case FontAwesomeIconType.SOLID_ANGLE_DOUBLE_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_ANGLE_DOUBLE_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_ANGLE_DOUBLE_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_ANGLE_DOUBLE_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_ANGLE_DOUBLE_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_ANGLE_DOUBLE_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_ANGLE_DOUBLE_UP:
                    unicode = FontAwesomeUnicode.SOLID_ANGLE_DOUBLE_UP;
                    break;

                case FontAwesomeIconType.SOLID_ANGLE_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_ANGLE_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_ANGLE_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_ANGLE_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_ANGLE_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_ANGLE_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_ANGLE_UP:
                    unicode = FontAwesomeUnicode.SOLID_ANGLE_UP;
                    break;

                case FontAwesomeIconType.SOLID_ANGRY:
                    unicode = FontAwesomeUnicode.SOLID_ANGRY;
                    break;

                case FontAwesomeIconType.SOLID_ANKH:
                    unicode = FontAwesomeUnicode.SOLID_ANKH;
                    break;

                case FontAwesomeIconType.SOLID_APPLE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_APPLE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_ARCHIVE:
                    unicode = FontAwesomeUnicode.SOLID_ARCHIVE;
                    break;

                case FontAwesomeIconType.SOLID_ARCHWAY:
                    unicode = FontAwesomeUnicode.SOLID_ARCHWAY;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_ALT_CIRCLE_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_ALT_CIRCLE_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_ALT_CIRCLE_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_ALT_CIRCLE_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_ALT_CIRCLE_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_ALT_CIRCLE_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_ALT_CIRCLE_UP:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_ALT_CIRCLE_UP;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_CIRCLE_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_CIRCLE_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_CIRCLE_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_CIRCLE_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_CIRCLE_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_CIRCLE_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_CIRCLE_UP:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_CIRCLE_UP;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_ARROW_UP:
                    unicode = FontAwesomeUnicode.SOLID_ARROW_UP;
                    break;

                case FontAwesomeIconType.SOLID_ARROWS_ALT:
                    unicode = FontAwesomeUnicode.SOLID_ARROWS_ALT;
                    break;

                case FontAwesomeIconType.SOLID_ARROWS_ALT_H:
                    unicode = FontAwesomeUnicode.SOLID_ARROWS_ALT_H;
                    break;

                case FontAwesomeIconType.SOLID_ARROWS_ALT_V:
                    unicode = FontAwesomeUnicode.SOLID_ARROWS_ALT_V;
                    break;

                case FontAwesomeIconType.SOLID_ASSISTIVE_LISTENING_SYSTEMS:
                    unicode = FontAwesomeUnicode.SOLID_ASSISTIVE_LISTENING_SYSTEMS;
                    break;

                case FontAwesomeIconType.SOLID_ASTERISK:
                    unicode = FontAwesomeUnicode.SOLID_ASTERISK;
                    break;

                case FontAwesomeIconType.SOLID_AT:
                    unicode = FontAwesomeUnicode.SOLID_AT;
                    break;

                case FontAwesomeIconType.SOLID_ATLAS:
                    unicode = FontAwesomeUnicode.SOLID_ATLAS;
                    break;

                case FontAwesomeIconType.SOLID_ATOM:
                    unicode = FontAwesomeUnicode.SOLID_ATOM;
                    break;

                case FontAwesomeIconType.SOLID_AUDIO_DESCRIPTION:
                    unicode = FontAwesomeUnicode.SOLID_AUDIO_DESCRIPTION;
                    break;

                case FontAwesomeIconType.SOLID_AWARD:
                    unicode = FontAwesomeUnicode.SOLID_AWARD;
                    break;

                case FontAwesomeIconType.SOLID_BACKSPACE:
                    unicode = FontAwesomeUnicode.SOLID_BACKSPACE;
                    break;

                case FontAwesomeIconType.SOLID_BACKWARD:
                    unicode = FontAwesomeUnicode.SOLID_BACKWARD;
                    break;

                case FontAwesomeIconType.SOLID_BALANCE_SCALE:
                    unicode = FontAwesomeUnicode.SOLID_BALANCE_SCALE;
                    break;

                case FontAwesomeIconType.SOLID_BAN:
                    unicode = FontAwesomeUnicode.SOLID_BAN;
                    break;

                case FontAwesomeIconType.SOLID_BAND_AID:
                    unicode = FontAwesomeUnicode.SOLID_BAND_AID;
                    break;

                case FontAwesomeIconType.SOLID_BARCODE:
                    unicode = FontAwesomeUnicode.SOLID_BARCODE;
                    break;

                case FontAwesomeIconType.SOLID_BARS:
                    unicode = FontAwesomeUnicode.SOLID_BARS;
                    break;

                case FontAwesomeIconType.SOLID_BASEBALL_BALL:
                    unicode = FontAwesomeUnicode.SOLID_BASEBALL_BALL;
                    break;

                case FontAwesomeIconType.SOLID_BASKETBALL_BALL:
                    unicode = FontAwesomeUnicode.SOLID_BASKETBALL_BALL;
                    break;

                case FontAwesomeIconType.SOLID_BATH:
                    unicode = FontAwesomeUnicode.SOLID_BATH;
                    break;

                case FontAwesomeIconType.SOLID_BATTERY_EMPTY:
                    unicode = FontAwesomeUnicode.SOLID_BATTERY_EMPTY;
                    break;

                case FontAwesomeIconType.SOLID_BATTERY_FULL:
                    unicode = FontAwesomeUnicode.SOLID_BATTERY_FULL;
                    break;

                case FontAwesomeIconType.SOLID_BATTERY_HALF:
                    unicode = FontAwesomeUnicode.SOLID_BATTERY_HALF;
                    break;

                case FontAwesomeIconType.SOLID_BATTERY_QUARTER:
                    unicode = FontAwesomeUnicode.SOLID_BATTERY_QUARTER;
                    break;

                case FontAwesomeIconType.SOLID_BATTERY_THREE_QUARTERS:
                    unicode = FontAwesomeUnicode.SOLID_BATTERY_THREE_QUARTERS;
                    break;

                case FontAwesomeIconType.SOLID_BED:
                    unicode = FontAwesomeUnicode.SOLID_BED;
                    break;

                case FontAwesomeIconType.SOLID_BEER:
                    unicode = FontAwesomeUnicode.SOLID_BEER;
                    break;

                case FontAwesomeIconType.SOLID_BELL:
                    unicode = FontAwesomeUnicode.SOLID_BELL;
                    break;

                case FontAwesomeIconType.SOLID_BELL_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_BELL_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_BEZIER_CURVE:
                    unicode = FontAwesomeUnicode.SOLID_BEZIER_CURVE;
                    break;

                case FontAwesomeIconType.SOLID_BIBLE:
                    unicode = FontAwesomeUnicode.SOLID_BIBLE;
                    break;

                case FontAwesomeIconType.SOLID_BICYCLE:
                    unicode = FontAwesomeUnicode.SOLID_BICYCLE;
                    break;

                case FontAwesomeIconType.SOLID_BINOCULARS:
                    unicode = FontAwesomeUnicode.SOLID_BINOCULARS;
                    break;

                case FontAwesomeIconType.SOLID_BIRTHDAY_CAKE:
                    unicode = FontAwesomeUnicode.SOLID_BIRTHDAY_CAKE;
                    break;

                case FontAwesomeIconType.SOLID_BLENDER:
                    unicode = FontAwesomeUnicode.SOLID_BLENDER;
                    break;

                case FontAwesomeIconType.SOLID_BLENDER_PHONE:
                    unicode = FontAwesomeUnicode.SOLID_BLENDER_PHONE;
                    break;

                case FontAwesomeIconType.SOLID_BLIND:
                    unicode = FontAwesomeUnicode.SOLID_BLIND;
                    break;

                case FontAwesomeIconType.SOLID_BOLD:
                    unicode = FontAwesomeUnicode.SOLID_BOLD;
                    break;

                case FontAwesomeIconType.SOLID_BOLT:
                    unicode = FontAwesomeUnicode.SOLID_BOLT;
                    break;

                case FontAwesomeIconType.SOLID_BOMB:
                    unicode = FontAwesomeUnicode.SOLID_BOMB;
                    break;

                case FontAwesomeIconType.SOLID_BONE:
                    unicode = FontAwesomeUnicode.SOLID_BONE;
                    break;

                case FontAwesomeIconType.SOLID_BONG:
                    unicode = FontAwesomeUnicode.SOLID_BONG;
                    break;

                case FontAwesomeIconType.SOLID_BOOK:
                    unicode = FontAwesomeUnicode.SOLID_BOOK;
                    break;

                case FontAwesomeIconType.SOLID_BOOK_DEAD:
                    unicode = FontAwesomeUnicode.SOLID_BOOK_DEAD;
                    break;

                case FontAwesomeIconType.SOLID_BOOK_OPEN:
                    unicode = FontAwesomeUnicode.SOLID_BOOK_OPEN;
                    break;

                case FontAwesomeIconType.SOLID_BOOK_READER:
                    unicode = FontAwesomeUnicode.SOLID_BOOK_READER;
                    break;

                case FontAwesomeIconType.SOLID_BOOKMARK:
                    unicode = FontAwesomeUnicode.SOLID_BOOKMARK;
                    break;

                case FontAwesomeIconType.SOLID_BOWLING_BALL:
                    unicode = FontAwesomeUnicode.SOLID_BOWLING_BALL;
                    break;

                case FontAwesomeIconType.SOLID_BOX:
                    unicode = FontAwesomeUnicode.SOLID_BOX;
                    break;

                case FontAwesomeIconType.SOLID_BOX_OPEN:
                    unicode = FontAwesomeUnicode.SOLID_BOX_OPEN;
                    break;

                case FontAwesomeIconType.SOLID_BOXES:
                    unicode = FontAwesomeUnicode.SOLID_BOXES;
                    break;

                case FontAwesomeIconType.SOLID_BRAILLE:
                    unicode = FontAwesomeUnicode.SOLID_BRAILLE;
                    break;

                case FontAwesomeIconType.SOLID_BRAIN:
                    unicode = FontAwesomeUnicode.SOLID_BRAIN;
                    break;

                case FontAwesomeIconType.SOLID_BRIEFCASE:
                    unicode = FontAwesomeUnicode.SOLID_BRIEFCASE;
                    break;

                case FontAwesomeIconType.SOLID_BRIEFCASE_MEDICAL:
                    unicode = FontAwesomeUnicode.SOLID_BRIEFCASE_MEDICAL;
                    break;

                case FontAwesomeIconType.SOLID_BROADCAST_TOWER:
                    unicode = FontAwesomeUnicode.SOLID_BROADCAST_TOWER;
                    break;

                case FontAwesomeIconType.SOLID_BROOM:
                    unicode = FontAwesomeUnicode.SOLID_BROOM;
                    break;

                case FontAwesomeIconType.SOLID_BRUSH:
                    unicode = FontAwesomeUnicode.SOLID_BRUSH;
                    break;

                case FontAwesomeIconType.SOLID_BUG:
                    unicode = FontAwesomeUnicode.SOLID_BUG;
                    break;

                case FontAwesomeIconType.SOLID_BUILDING:
                    unicode = FontAwesomeUnicode.SOLID_BUILDING;
                    break;

                case FontAwesomeIconType.SOLID_BULLHORN:
                    unicode = FontAwesomeUnicode.SOLID_BULLHORN;
                    break;

                case FontAwesomeIconType.SOLID_BULLSEYE:
                    unicode = FontAwesomeUnicode.SOLID_BULLSEYE;
                    break;

                case FontAwesomeIconType.SOLID_BURN:
                    unicode = FontAwesomeUnicode.SOLID_BURN;
                    break;

                case FontAwesomeIconType.SOLID_BUS:
                    unicode = FontAwesomeUnicode.SOLID_BUS;
                    break;

                case FontAwesomeIconType.SOLID_BUS_ALT:
                    unicode = FontAwesomeUnicode.SOLID_BUS_ALT;
                    break;

                case FontAwesomeIconType.SOLID_BUSINESS_TIME:
                    unicode = FontAwesomeUnicode.SOLID_BUSINESS_TIME;
                    break;

                case FontAwesomeIconType.SOLID_CALCULATOR:
                    unicode = FontAwesomeUnicode.SOLID_CALCULATOR;
                    break;

                case FontAwesomeIconType.SOLID_CALENDAR:
                    unicode = FontAwesomeUnicode.SOLID_CALENDAR;
                    break;

                case FontAwesomeIconType.SOLID_CALENDAR_ALT:
                    unicode = FontAwesomeUnicode.SOLID_CALENDAR_ALT;
                    break;

                case FontAwesomeIconType.SOLID_CALENDAR_CHECK:
                    unicode = FontAwesomeUnicode.SOLID_CALENDAR_CHECK;
                    break;

                case FontAwesomeIconType.SOLID_CALENDAR_MINUS:
                    unicode = FontAwesomeUnicode.SOLID_CALENDAR_MINUS;
                    break;

                case FontAwesomeIconType.SOLID_CALENDAR_PLUS:
                    unicode = FontAwesomeUnicode.SOLID_CALENDAR_PLUS;
                    break;

                case FontAwesomeIconType.SOLID_CALENDAR_TIMES:
                    unicode = FontAwesomeUnicode.SOLID_CALENDAR_TIMES;
                    break;

                case FontAwesomeIconType.SOLID_CAMERA:
                    unicode = FontAwesomeUnicode.SOLID_CAMERA;
                    break;

                case FontAwesomeIconType.SOLID_CAMERA_RETRO:
                    unicode = FontAwesomeUnicode.SOLID_CAMERA_RETRO;
                    break;

                case FontAwesomeIconType.SOLID_CAMPGROUND:
                    unicode = FontAwesomeUnicode.SOLID_CAMPGROUND;
                    break;

                case FontAwesomeIconType.SOLID_CANNABIS:
                    unicode = FontAwesomeUnicode.SOLID_CANNABIS;
                    break;

                case FontAwesomeIconType.SOLID_CAPSULES:
                    unicode = FontAwesomeUnicode.SOLID_CAPSULES;
                    break;

                case FontAwesomeIconType.SOLID_CAR:
                    unicode = FontAwesomeUnicode.SOLID_CAR;
                    break;

                case FontAwesomeIconType.SOLID_CAR_ALT:
                    unicode = FontAwesomeUnicode.SOLID_CAR_ALT;
                    break;

                case FontAwesomeIconType.SOLID_CAR_BATTERY:
                    unicode = FontAwesomeUnicode.SOLID_CAR_BATTERY;
                    break;

                case FontAwesomeIconType.SOLID_CAR_CRASH:
                    unicode = FontAwesomeUnicode.SOLID_CAR_CRASH;
                    break;

                case FontAwesomeIconType.SOLID_CAR_SIDE:
                    unicode = FontAwesomeUnicode.SOLID_CAR_SIDE;
                    break;

                case FontAwesomeIconType.SOLID_CARET_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_CARET_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_CARET_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_CARET_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_CARET_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_CARET_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_CARET_SQUARE_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_CARET_SQUARE_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_CARET_SQUARE_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_CARET_SQUARE_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_CARET_SQUARE_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_CARET_SQUARE_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_CARET_SQUARE_UP:
                    unicode = FontAwesomeUnicode.SOLID_CARET_SQUARE_UP;
                    break;

                case FontAwesomeIconType.SOLID_CARET_UP:
                    unicode = FontAwesomeUnicode.SOLID_CARET_UP;
                    break;

                case FontAwesomeIconType.SOLID_CART_ARROW_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_CART_ARROW_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_CART_PLUS:
                    unicode = FontAwesomeUnicode.SOLID_CART_PLUS;
                    break;

                case FontAwesomeIconType.SOLID_CAT:
                    unicode = FontAwesomeUnicode.SOLID_CAT;
                    break;

                case FontAwesomeIconType.SOLID_CERTIFICATE:
                    unicode = FontAwesomeUnicode.SOLID_CERTIFICATE;
                    break;

                case FontAwesomeIconType.SOLID_CHAIR:
                    unicode = FontAwesomeUnicode.SOLID_CHAIR;
                    break;

                case FontAwesomeIconType.SOLID_CHALKBOARD:
                    unicode = FontAwesomeUnicode.SOLID_CHALKBOARD;
                    break;

                case FontAwesomeIconType.SOLID_CHALKBOARD_TEACHER:
                    unicode = FontAwesomeUnicode.SOLID_CHALKBOARD_TEACHER;
                    break;

                case FontAwesomeIconType.SOLID_CHARGING_STATION:
                    unicode = FontAwesomeUnicode.SOLID_CHARGING_STATION;
                    break;

                case FontAwesomeIconType.SOLID_CHART_AREA:
                    unicode = FontAwesomeUnicode.SOLID_CHART_AREA;
                    break;

                case FontAwesomeIconType.SOLID_CHART_BAR:
                    unicode = FontAwesomeUnicode.SOLID_CHART_BAR;
                    break;

                case FontAwesomeIconType.SOLID_CHART_LINE:
                    unicode = FontAwesomeUnicode.SOLID_CHART_LINE;
                    break;

                case FontAwesomeIconType.SOLID_CHART_PIE:
                    unicode = FontAwesomeUnicode.SOLID_CHART_PIE;
                    break;

                case FontAwesomeIconType.SOLID_CHECK:
                    unicode = FontAwesomeUnicode.SOLID_CHECK;
                    break;

                case FontAwesomeIconType.SOLID_CHECK_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_CHECK_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_CHECK_DOUBLE:
                    unicode = FontAwesomeUnicode.SOLID_CHECK_DOUBLE;
                    break;

                case FontAwesomeIconType.SOLID_CHECK_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_CHECK_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_CHESS:
                    unicode = FontAwesomeUnicode.SOLID_CHESS;
                    break;

                case FontAwesomeIconType.SOLID_CHESS_BISHOP:
                    unicode = FontAwesomeUnicode.SOLID_CHESS_BISHOP;
                    break;

                case FontAwesomeIconType.SOLID_CHESS_BOARD:
                    unicode = FontAwesomeUnicode.SOLID_CHESS_BOARD;
                    break;

                case FontAwesomeIconType.SOLID_CHESS_KING:
                    unicode = FontAwesomeUnicode.SOLID_CHESS_KING;
                    break;

                case FontAwesomeIconType.SOLID_CHESS_KNIGHT:
                    unicode = FontAwesomeUnicode.SOLID_CHESS_KNIGHT;
                    break;

                case FontAwesomeIconType.SOLID_CHESS_PAWN:
                    unicode = FontAwesomeUnicode.SOLID_CHESS_PAWN;
                    break;

                case FontAwesomeIconType.SOLID_CHESS_QUEEN:
                    unicode = FontAwesomeUnicode.SOLID_CHESS_QUEEN;
                    break;

                case FontAwesomeIconType.SOLID_CHESS_ROOK:
                    unicode = FontAwesomeUnicode.SOLID_CHESS_ROOK;
                    break;

                case FontAwesomeIconType.SOLID_CHEVRON_CIRCLE_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_CHEVRON_CIRCLE_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_CHEVRON_CIRCLE_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_CHEVRON_CIRCLE_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_CHEVRON_CIRCLE_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_CHEVRON_CIRCLE_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_CHEVRON_CIRCLE_UP:
                    unicode = FontAwesomeUnicode.SOLID_CHEVRON_CIRCLE_UP;
                    break;

                case FontAwesomeIconType.SOLID_CHEVRON_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_CHEVRON_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_CHEVRON_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_CHEVRON_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_CHEVRON_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_CHEVRON_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_CHEVRON_UP:
                    unicode = FontAwesomeUnicode.SOLID_CHEVRON_UP;
                    break;

                case FontAwesomeIconType.SOLID_CHILD:
                    unicode = FontAwesomeUnicode.SOLID_CHILD;
                    break;

                case FontAwesomeIconType.SOLID_CHURCH:
                    unicode = FontAwesomeUnicode.SOLID_CHURCH;
                    break;

                case FontAwesomeIconType.SOLID_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_CIRCLE_NOTCH:
                    unicode = FontAwesomeUnicode.SOLID_CIRCLE_NOTCH;
                    break;

                case FontAwesomeIconType.SOLID_CITY:
                    unicode = FontAwesomeUnicode.SOLID_CITY;
                    break;

                case FontAwesomeIconType.SOLID_CLIPBOARD:
                    unicode = FontAwesomeUnicode.SOLID_CLIPBOARD;
                    break;

                case FontAwesomeIconType.SOLID_CLIPBOARD_CHECK:
                    unicode = FontAwesomeUnicode.SOLID_CLIPBOARD_CHECK;
                    break;

                case FontAwesomeIconType.SOLID_CLIPBOARD_LIST:
                    unicode = FontAwesomeUnicode.SOLID_CLIPBOARD_LIST;
                    break;

                case FontAwesomeIconType.SOLID_CLOCK:
                    unicode = FontAwesomeUnicode.SOLID_CLOCK;
                    break;

                case FontAwesomeIconType.SOLID_CLONE:
                    unicode = FontAwesomeUnicode.SOLID_CLONE;
                    break;

                case FontAwesomeIconType.SOLID_CLOSED_CAPTIONING:
                    unicode = FontAwesomeUnicode.SOLID_CLOSED_CAPTIONING;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_DOWNLOAD_ALT:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_DOWNLOAD_ALT;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_MEATBALL:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_MEATBALL;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_MOON:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_MOON;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_MOON_RAIN:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_MOON_RAIN;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_RAIN:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_RAIN;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_SHOWERS_HEAVY:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_SHOWERS_HEAVY;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_SUN:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_SUN;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_SUN_RAIN:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_SUN_RAIN;
                    break;

                case FontAwesomeIconType.SOLID_CLOUD_UPLOAD_ALT:
                    unicode = FontAwesomeUnicode.SOLID_CLOUD_UPLOAD_ALT;
                    break;

                case FontAwesomeIconType.SOLID_COCKTAIL:
                    unicode = FontAwesomeUnicode.SOLID_COCKTAIL;
                    break;

                case FontAwesomeIconType.SOLID_CODE:
                    unicode = FontAwesomeUnicode.SOLID_CODE;
                    break;

                case FontAwesomeIconType.SOLID_CODE_BRANCH:
                    unicode = FontAwesomeUnicode.SOLID_CODE_BRANCH;
                    break;

                case FontAwesomeIconType.SOLID_COFFEE:
                    unicode = FontAwesomeUnicode.SOLID_COFFEE;
                    break;

                case FontAwesomeIconType.SOLID_COG:
                    unicode = FontAwesomeUnicode.SOLID_COG;
                    break;

                case FontAwesomeIconType.SOLID_COGS:
                    unicode = FontAwesomeUnicode.SOLID_COGS;
                    break;

                case FontAwesomeIconType.SOLID_COINS:
                    unicode = FontAwesomeUnicode.SOLID_COINS;
                    break;

                case FontAwesomeIconType.SOLID_COLUMNS:
                    unicode = FontAwesomeUnicode.SOLID_COLUMNS;
                    break;

                case FontAwesomeIconType.SOLID_COMMENT:
                    unicode = FontAwesomeUnicode.SOLID_COMMENT;
                    break;

                case FontAwesomeIconType.SOLID_COMMENT_ALT:
                    unicode = FontAwesomeUnicode.SOLID_COMMENT_ALT;
                    break;

                case FontAwesomeIconType.SOLID_COMMENT_DOLLAR:
                    unicode = FontAwesomeUnicode.SOLID_COMMENT_DOLLAR;
                    break;

                case FontAwesomeIconType.SOLID_COMMENT_DOTS:
                    unicode = FontAwesomeUnicode.SOLID_COMMENT_DOTS;
                    break;

                case FontAwesomeIconType.SOLID_COMMENT_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_COMMENT_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_COMMENTS:
                    unicode = FontAwesomeUnicode.SOLID_COMMENTS;
                    break;

                case FontAwesomeIconType.SOLID_COMMENTS_DOLLAR:
                    unicode = FontAwesomeUnicode.SOLID_COMMENTS_DOLLAR;
                    break;

                case FontAwesomeIconType.SOLID_COMPACT_DISC:
                    unicode = FontAwesomeUnicode.SOLID_COMPACT_DISC;
                    break;

                case FontAwesomeIconType.SOLID_COMPASS:
                    unicode = FontAwesomeUnicode.SOLID_COMPASS;
                    break;

                case FontAwesomeIconType.SOLID_COMPRESS:
                    unicode = FontAwesomeUnicode.SOLID_COMPRESS;
                    break;

                case FontAwesomeIconType.SOLID_CONCIERGE_BELL:
                    unicode = FontAwesomeUnicode.SOLID_CONCIERGE_BELL;
                    break;

                case FontAwesomeIconType.SOLID_COOKIE:
                    unicode = FontAwesomeUnicode.SOLID_COOKIE;
                    break;

                case FontAwesomeIconType.SOLID_COOKIE_BITE:
                    unicode = FontAwesomeUnicode.SOLID_COOKIE_BITE;
                    break;

                case FontAwesomeIconType.SOLID_COPY:
                    unicode = FontAwesomeUnicode.SOLID_COPY;
                    break;

                case FontAwesomeIconType.SOLID_COPYRIGHT:
                    unicode = FontAwesomeUnicode.SOLID_COPYRIGHT;
                    break;

                case FontAwesomeIconType.SOLID_COUCH:
                    unicode = FontAwesomeUnicode.SOLID_COUCH;
                    break;

                case FontAwesomeIconType.SOLID_CREDIT_CARD:
                    unicode = FontAwesomeUnicode.SOLID_CREDIT_CARD;
                    break;

                case FontAwesomeIconType.SOLID_CROP:
                    unicode = FontAwesomeUnicode.SOLID_CROP;
                    break;

                case FontAwesomeIconType.SOLID_CROP_ALT:
                    unicode = FontAwesomeUnicode.SOLID_CROP_ALT;
                    break;

                case FontAwesomeIconType.SOLID_CROSS:
                    unicode = FontAwesomeUnicode.SOLID_CROSS;
                    break;

                case FontAwesomeIconType.SOLID_CROSSHAIRS:
                    unicode = FontAwesomeUnicode.SOLID_CROSSHAIRS;
                    break;

                case FontAwesomeIconType.SOLID_CROW:
                    unicode = FontAwesomeUnicode.SOLID_CROW;
                    break;

                case FontAwesomeIconType.SOLID_CROWN:
                    unicode = FontAwesomeUnicode.SOLID_CROWN;
                    break;

                case FontAwesomeIconType.SOLID_CUBE:
                    unicode = FontAwesomeUnicode.SOLID_CUBE;
                    break;

                case FontAwesomeIconType.SOLID_CUBES:
                    unicode = FontAwesomeUnicode.SOLID_CUBES;
                    break;

                case FontAwesomeIconType.SOLID_CUT:
                    unicode = FontAwesomeUnicode.SOLID_CUT;
                    break;

                case FontAwesomeIconType.SOLID_DATABASE:
                    unicode = FontAwesomeUnicode.SOLID_DATABASE;
                    break;

                case FontAwesomeIconType.SOLID_DEAF:
                    unicode = FontAwesomeUnicode.SOLID_DEAF;
                    break;

                case FontAwesomeIconType.SOLID_DEMOCRAT:
                    unicode = FontAwesomeUnicode.SOLID_DEMOCRAT;
                    break;

                case FontAwesomeIconType.SOLID_DESKTOP:
                    unicode = FontAwesomeUnicode.SOLID_DESKTOP;
                    break;

                case FontAwesomeIconType.SOLID_DHARMACHAKRA:
                    unicode = FontAwesomeUnicode.SOLID_DHARMACHAKRA;
                    break;

                case FontAwesomeIconType.SOLID_DIAGNOSES:
                    unicode = FontAwesomeUnicode.SOLID_DIAGNOSES;
                    break;

                case FontAwesomeIconType.SOLID_DICE:
                    unicode = FontAwesomeUnicode.SOLID_DICE;
                    break;

                case FontAwesomeIconType.SOLID_DICE_D20:
                    unicode = FontAwesomeUnicode.SOLID_DICE_D20;
                    break;

                case FontAwesomeIconType.SOLID_DICE_D6:
                    unicode = FontAwesomeUnicode.SOLID_DICE_D6;
                    break;

                case FontAwesomeIconType.SOLID_DICE_FIVE:
                    unicode = FontAwesomeUnicode.SOLID_DICE_FIVE;
                    break;

                case FontAwesomeIconType.SOLID_DICE_FOUR:
                    unicode = FontAwesomeUnicode.SOLID_DICE_FOUR;
                    break;

                case FontAwesomeIconType.SOLID_DICE_ONE:
                    unicode = FontAwesomeUnicode.SOLID_DICE_ONE;
                    break;

                case FontAwesomeIconType.SOLID_DICE_SIX:
                    unicode = FontAwesomeUnicode.SOLID_DICE_SIX;
                    break;

                case FontAwesomeIconType.SOLID_DICE_THREE:
                    unicode = FontAwesomeUnicode.SOLID_DICE_THREE;
                    break;

                case FontAwesomeIconType.SOLID_DICE_TWO:
                    unicode = FontAwesomeUnicode.SOLID_DICE_TWO;
                    break;

                case FontAwesomeIconType.SOLID_DIGITAL_TACHOGRAPH:
                    unicode = FontAwesomeUnicode.SOLID_DIGITAL_TACHOGRAPH;
                    break;

                case FontAwesomeIconType.SOLID_DIRECTIONS:
                    unicode = FontAwesomeUnicode.SOLID_DIRECTIONS;
                    break;

                case FontAwesomeIconType.SOLID_DIVIDE:
                    unicode = FontAwesomeUnicode.SOLID_DIVIDE;
                    break;

                case FontAwesomeIconType.SOLID_DIZZY:
                    unicode = FontAwesomeUnicode.SOLID_DIZZY;
                    break;

                case FontAwesomeIconType.SOLID_DNA:
                    unicode = FontAwesomeUnicode.SOLID_DNA;
                    break;

                case FontAwesomeIconType.SOLID_DOG:
                    unicode = FontAwesomeUnicode.SOLID_DOG;
                    break;

                case FontAwesomeIconType.SOLID_DOLLAR_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_DOLLAR_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_DOLLY:
                    unicode = FontAwesomeUnicode.SOLID_DOLLY;
                    break;

                case FontAwesomeIconType.SOLID_DOLLY_FLATBED:
                    unicode = FontAwesomeUnicode.SOLID_DOLLY_FLATBED;
                    break;

                case FontAwesomeIconType.SOLID_DONATE:
                    unicode = FontAwesomeUnicode.SOLID_DONATE;
                    break;

                case FontAwesomeIconType.SOLID_DOOR_CLOSED:
                    unicode = FontAwesomeUnicode.SOLID_DOOR_CLOSED;
                    break;

                case FontAwesomeIconType.SOLID_DOOR_OPEN:
                    unicode = FontAwesomeUnicode.SOLID_DOOR_OPEN;
                    break;

                case FontAwesomeIconType.SOLID_DOT_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_DOT_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_DOVE:
                    unicode = FontAwesomeUnicode.SOLID_DOVE;
                    break;

                case FontAwesomeIconType.SOLID_DOWNLOAD:
                    unicode = FontAwesomeUnicode.SOLID_DOWNLOAD;
                    break;

                case FontAwesomeIconType.SOLID_DRAFTING_COMPASS:
                    unicode = FontAwesomeUnicode.SOLID_DRAFTING_COMPASS;
                    break;

                case FontAwesomeIconType.SOLID_DRAGON:
                    unicode = FontAwesomeUnicode.SOLID_DRAGON;
                    break;

                case FontAwesomeIconType.SOLID_DRAW_POLYGON:
                    unicode = FontAwesomeUnicode.SOLID_DRAW_POLYGON;
                    break;

                case FontAwesomeIconType.SOLID_DRUM:
                    unicode = FontAwesomeUnicode.SOLID_DRUM;
                    break;

                case FontAwesomeIconType.SOLID_DRUM_STEELPAN:
                    unicode = FontAwesomeUnicode.SOLID_DRUM_STEELPAN;
                    break;

                case FontAwesomeIconType.SOLID_DRUMSTICK_BITE:
                    unicode = FontAwesomeUnicode.SOLID_DRUMSTICK_BITE;
                    break;

                case FontAwesomeIconType.SOLID_DUMBBELL:
                    unicode = FontAwesomeUnicode.SOLID_DUMBBELL;
                    break;

                case FontAwesomeIconType.SOLID_DUNGEON:
                    unicode = FontAwesomeUnicode.SOLID_DUNGEON;
                    break;

                case FontAwesomeIconType.SOLID_EDIT:
                    unicode = FontAwesomeUnicode.SOLID_EDIT;
                    break;

                case FontAwesomeIconType.SOLID_EJECT:
                    unicode = FontAwesomeUnicode.SOLID_EJECT;
                    break;

                case FontAwesomeIconType.SOLID_ELLIPSIS_H:
                    unicode = FontAwesomeUnicode.SOLID_ELLIPSIS_H;
                    break;

                case FontAwesomeIconType.SOLID_ELLIPSIS_V:
                    unicode = FontAwesomeUnicode.SOLID_ELLIPSIS_V;
                    break;

                case FontAwesomeIconType.SOLID_ENVELOPE:
                    unicode = FontAwesomeUnicode.SOLID_ENVELOPE;
                    break;

                case FontAwesomeIconType.SOLID_ENVELOPE_OPEN:
                    unicode = FontAwesomeUnicode.SOLID_ENVELOPE_OPEN;
                    break;

                case FontAwesomeIconType.SOLID_ENVELOPE_OPEN_TEXT:
                    unicode = FontAwesomeUnicode.SOLID_ENVELOPE_OPEN_TEXT;
                    break;

                case FontAwesomeIconType.SOLID_ENVELOPE_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_ENVELOPE_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_EQUALS:
                    unicode = FontAwesomeUnicode.SOLID_EQUALS;
                    break;

                case FontAwesomeIconType.SOLID_ERASER:
                    unicode = FontAwesomeUnicode.SOLID_ERASER;
                    break;

                case FontAwesomeIconType.SOLID_EURO_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_EURO_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_EXCHANGE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_EXCHANGE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_EXCLAMATION:
                    unicode = FontAwesomeUnicode.SOLID_EXCLAMATION;
                    break;

                case FontAwesomeIconType.SOLID_EXCLAMATION_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_EXCLAMATION_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_EXCLAMATION_TRIANGLE:
                    unicode = FontAwesomeUnicode.SOLID_EXCLAMATION_TRIANGLE;
                    break;

                case FontAwesomeIconType.SOLID_EXPAND:
                    unicode = FontAwesomeUnicode.SOLID_EXPAND;
                    break;

                case FontAwesomeIconType.SOLID_EXPAND_ARROWS_ALT:
                    unicode = FontAwesomeUnicode.SOLID_EXPAND_ARROWS_ALT;
                    break;

                case FontAwesomeIconType.SOLID_EXTERNAL_LINK_ALT:
                    unicode = FontAwesomeUnicode.SOLID_EXTERNAL_LINK_ALT;
                    break;

                case FontAwesomeIconType.SOLID_EXTERNAL_LINK_SQUARE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_EXTERNAL_LINK_SQUARE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_EYE:
                    unicode = FontAwesomeUnicode.SOLID_EYE;
                    break;

                case FontAwesomeIconType.SOLID_EYE_DROPPER:
                    unicode = FontAwesomeUnicode.SOLID_EYE_DROPPER;
                    break;

                case FontAwesomeIconType.SOLID_EYE_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_EYE_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_FAST_BACKWARD:
                    unicode = FontAwesomeUnicode.SOLID_FAST_BACKWARD;
                    break;

                case FontAwesomeIconType.SOLID_FAST_FORWARD:
                    unicode = FontAwesomeUnicode.SOLID_FAST_FORWARD;
                    break;

                case FontAwesomeIconType.SOLID_FAX:
                    unicode = FontAwesomeUnicode.SOLID_FAX;
                    break;

                case FontAwesomeIconType.SOLID_FEATHER:
                    unicode = FontAwesomeUnicode.SOLID_FEATHER;
                    break;

                case FontAwesomeIconType.SOLID_FEATHER_ALT:
                    unicode = FontAwesomeUnicode.SOLID_FEATHER_ALT;
                    break;

                case FontAwesomeIconType.SOLID_FEMALE:
                    unicode = FontAwesomeUnicode.SOLID_FEMALE;
                    break;

                case FontAwesomeIconType.SOLID_FIGHTER_JET:
                    unicode = FontAwesomeUnicode.SOLID_FIGHTER_JET;
                    break;

                case FontAwesomeIconType.SOLID_FILE:
                    unicode = FontAwesomeUnicode.SOLID_FILE;
                    break;

                case FontAwesomeIconType.SOLID_FILE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_FILE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_FILE_ARCHIVE:
                    unicode = FontAwesomeUnicode.SOLID_FILE_ARCHIVE;
                    break;

                case FontAwesomeIconType.SOLID_FILE_AUDIO:
                    unicode = FontAwesomeUnicode.SOLID_FILE_AUDIO;
                    break;

                case FontAwesomeIconType.SOLID_FILE_CODE:
                    unicode = FontAwesomeUnicode.SOLID_FILE_CODE;
                    break;

                case FontAwesomeIconType.SOLID_FILE_CONTRACT:
                    unicode = FontAwesomeUnicode.SOLID_FILE_CONTRACT;
                    break;

                case FontAwesomeIconType.SOLID_FILE_CSV:
                    unicode = FontAwesomeUnicode.SOLID_FILE_CSV;
                    break;

                case FontAwesomeIconType.SOLID_FILE_DOWNLOAD:
                    unicode = FontAwesomeUnicode.SOLID_FILE_DOWNLOAD;
                    break;

                case FontAwesomeIconType.SOLID_FILE_EXCEL:
                    unicode = FontAwesomeUnicode.SOLID_FILE_EXCEL;
                    break;

                case FontAwesomeIconType.SOLID_FILE_EXPORT:
                    unicode = FontAwesomeUnicode.SOLID_FILE_EXPORT;
                    break;

                case FontAwesomeIconType.SOLID_FILE_IMAGE:
                    unicode = FontAwesomeUnicode.SOLID_FILE_IMAGE;
                    break;

                case FontAwesomeIconType.SOLID_FILE_IMPORT:
                    unicode = FontAwesomeUnicode.SOLID_FILE_IMPORT;
                    break;

                case FontAwesomeIconType.SOLID_FILE_INVOICE:
                    unicode = FontAwesomeUnicode.SOLID_FILE_INVOICE;
                    break;

                case FontAwesomeIconType.SOLID_FILE_INVOICE_DOLLAR:
                    unicode = FontAwesomeUnicode.SOLID_FILE_INVOICE_DOLLAR;
                    break;

                case FontAwesomeIconType.SOLID_FILE_MEDICAL:
                    unicode = FontAwesomeUnicode.SOLID_FILE_MEDICAL;
                    break;

                case FontAwesomeIconType.SOLID_FILE_MEDICAL_ALT:
                    unicode = FontAwesomeUnicode.SOLID_FILE_MEDICAL_ALT;
                    break;

                case FontAwesomeIconType.SOLID_FILE_PDF:
                    unicode = FontAwesomeUnicode.SOLID_FILE_PDF;
                    break;

                case FontAwesomeIconType.SOLID_FILE_POWERPOINT:
                    unicode = FontAwesomeUnicode.SOLID_FILE_POWERPOINT;
                    break;

                case FontAwesomeIconType.SOLID_FILE_PRESCRIPTION:
                    unicode = FontAwesomeUnicode.SOLID_FILE_PRESCRIPTION;
                    break;

                case FontAwesomeIconType.SOLID_FILE_SIGNATURE:
                    unicode = FontAwesomeUnicode.SOLID_FILE_SIGNATURE;
                    break;

                case FontAwesomeIconType.SOLID_FILE_UPLOAD:
                    unicode = FontAwesomeUnicode.SOLID_FILE_UPLOAD;
                    break;

                case FontAwesomeIconType.SOLID_FILE_VIDEO:
                    unicode = FontAwesomeUnicode.SOLID_FILE_VIDEO;
                    break;

                case FontAwesomeIconType.SOLID_FILE_WORD:
                    unicode = FontAwesomeUnicode.SOLID_FILE_WORD;
                    break;

                case FontAwesomeIconType.SOLID_FILL:
                    unicode = FontAwesomeUnicode.SOLID_FILL;
                    break;

                case FontAwesomeIconType.SOLID_FILL_DRIP:
                    unicode = FontAwesomeUnicode.SOLID_FILL_DRIP;
                    break;

                case FontAwesomeIconType.SOLID_FILM:
                    unicode = FontAwesomeUnicode.SOLID_FILM;
                    break;

                case FontAwesomeIconType.SOLID_FILTER:
                    unicode = FontAwesomeUnicode.SOLID_FILTER;
                    break;

                case FontAwesomeIconType.SOLID_FINGERPRINT:
                    unicode = FontAwesomeUnicode.SOLID_FINGERPRINT;
                    break;

                case FontAwesomeIconType.SOLID_FIRE:
                    unicode = FontAwesomeUnicode.SOLID_FIRE;
                    break;

                case FontAwesomeIconType.SOLID_FIRE_EXTINGUISHER:
                    unicode = FontAwesomeUnicode.SOLID_FIRE_EXTINGUISHER;
                    break;

                case FontAwesomeIconType.SOLID_FIRST_AID:
                    unicode = FontAwesomeUnicode.SOLID_FIRST_AID;
                    break;

                case FontAwesomeIconType.SOLID_FISH:
                    unicode = FontAwesomeUnicode.SOLID_FISH;
                    break;

                case FontAwesomeIconType.SOLID_FIST_RAISED:
                    unicode = FontAwesomeUnicode.SOLID_FIST_RAISED;
                    break;

                case FontAwesomeIconType.SOLID_FLAG:
                    unicode = FontAwesomeUnicode.SOLID_FLAG;
                    break;

                case FontAwesomeIconType.SOLID_FLAG_CHECKERED:
                    unicode = FontAwesomeUnicode.SOLID_FLAG_CHECKERED;
                    break;

                case FontAwesomeIconType.SOLID_FLAG_USA:
                    unicode = FontAwesomeUnicode.SOLID_FLAG_USA;
                    break;

                case FontAwesomeIconType.SOLID_FLASK:
                    unicode = FontAwesomeUnicode.SOLID_FLASK;
                    break;

                case FontAwesomeIconType.SOLID_FLUSHED:
                    unicode = FontAwesomeUnicode.SOLID_FLUSHED;
                    break;

                case FontAwesomeIconType.SOLID_FOLDER:
                    unicode = FontAwesomeUnicode.SOLID_FOLDER;
                    break;

                case FontAwesomeIconType.SOLID_FOLDER_MINUS:
                    unicode = FontAwesomeUnicode.SOLID_FOLDER_MINUS;
                    break;

                case FontAwesomeIconType.SOLID_FOLDER_OPEN:
                    unicode = FontAwesomeUnicode.SOLID_FOLDER_OPEN;
                    break;

                case FontAwesomeIconType.SOLID_FOLDER_PLUS:
                    unicode = FontAwesomeUnicode.SOLID_FOLDER_PLUS;
                    break;

                case FontAwesomeIconType.SOLID_FONT:
                    unicode = FontAwesomeUnicode.SOLID_FONT;
                    break;

                case FontAwesomeIconType.SOLID_FOOTBALL_BALL:
                    unicode = FontAwesomeUnicode.SOLID_FOOTBALL_BALL;
                    break;

                case FontAwesomeIconType.SOLID_FORWARD:
                    unicode = FontAwesomeUnicode.SOLID_FORWARD;
                    break;

                case FontAwesomeIconType.SOLID_FROG:
                    unicode = FontAwesomeUnicode.SOLID_FROG;
                    break;

                case FontAwesomeIconType.SOLID_FROWN:
                    unicode = FontAwesomeUnicode.SOLID_FROWN;
                    break;

                case FontAwesomeIconType.SOLID_FROWN_OPEN:
                    unicode = FontAwesomeUnicode.SOLID_FROWN_OPEN;
                    break;

                case FontAwesomeIconType.SOLID_FUNNEL_DOLLAR:
                    unicode = FontAwesomeUnicode.SOLID_FUNNEL_DOLLAR;
                    break;

                case FontAwesomeIconType.SOLID_FUTBOL:
                    unicode = FontAwesomeUnicode.SOLID_FUTBOL;
                    break;

                case FontAwesomeIconType.SOLID_GAMEPAD:
                    unicode = FontAwesomeUnicode.SOLID_GAMEPAD;
                    break;

                case FontAwesomeIconType.SOLID_GAS_PUMP:
                    unicode = FontAwesomeUnicode.SOLID_GAS_PUMP;
                    break;

                case FontAwesomeIconType.SOLID_GAVEL:
                    unicode = FontAwesomeUnicode.SOLID_GAVEL;
                    break;

                case FontAwesomeIconType.SOLID_GEM:
                    unicode = FontAwesomeUnicode.SOLID_GEM;
                    break;

                case FontAwesomeIconType.SOLID_GENDERLESS:
                    unicode = FontAwesomeUnicode.SOLID_GENDERLESS;
                    break;

                case FontAwesomeIconType.SOLID_GHOST:
                    unicode = FontAwesomeUnicode.SOLID_GHOST;
                    break;

                case FontAwesomeIconType.SOLID_GIFT:
                    unicode = FontAwesomeUnicode.SOLID_GIFT;
                    break;

                case FontAwesomeIconType.SOLID_GLASS_MARTINI:
                    unicode = FontAwesomeUnicode.SOLID_GLASS_MARTINI;
                    break;

                case FontAwesomeIconType.SOLID_GLASS_MARTINI_ALT:
                    unicode = FontAwesomeUnicode.SOLID_GLASS_MARTINI_ALT;
                    break;

                case FontAwesomeIconType.SOLID_GLASSES:
                    unicode = FontAwesomeUnicode.SOLID_GLASSES;
                    break;

                case FontAwesomeIconType.SOLID_GLOBE:
                    unicode = FontAwesomeUnicode.SOLID_GLOBE;
                    break;

                case FontAwesomeIconType.SOLID_GLOBE_AFRICA:
                    unicode = FontAwesomeUnicode.SOLID_GLOBE_AFRICA;
                    break;

                case FontAwesomeIconType.SOLID_GLOBE_AMERICAS:
                    unicode = FontAwesomeUnicode.SOLID_GLOBE_AMERICAS;
                    break;

                case FontAwesomeIconType.SOLID_GLOBE_ASIA:
                    unicode = FontAwesomeUnicode.SOLID_GLOBE_ASIA;
                    break;

                case FontAwesomeIconType.SOLID_GOLF_BALL:
                    unicode = FontAwesomeUnicode.SOLID_GOLF_BALL;
                    break;

                case FontAwesomeIconType.SOLID_GOPURAM:
                    unicode = FontAwesomeUnicode.SOLID_GOPURAM;
                    break;

                case FontAwesomeIconType.SOLID_GRADUATION_CAP:
                    unicode = FontAwesomeUnicode.SOLID_GRADUATION_CAP;
                    break;

                case FontAwesomeIconType.SOLID_GREATER_THAN:
                    unicode = FontAwesomeUnicode.SOLID_GREATER_THAN;
                    break;

                case FontAwesomeIconType.SOLID_GREATER_THAN_EQUAL:
                    unicode = FontAwesomeUnicode.SOLID_GREATER_THAN_EQUAL;
                    break;

                case FontAwesomeIconType.SOLID_GRIMACE:
                    unicode = FontAwesomeUnicode.SOLID_GRIMACE;
                    break;

                case FontAwesomeIconType.SOLID_GRIN:
                    unicode = FontAwesomeUnicode.SOLID_GRIN;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_ALT:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_ALT;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_BEAM:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_BEAM;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_BEAM_SWEAT:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_BEAM_SWEAT;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_HEARTS:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_HEARTS;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_SQUINT:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_SQUINT;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_SQUINT_TEARS:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_SQUINT_TEARS;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_STARS:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_STARS;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_TEARS:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_TEARS;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_TONGUE:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_TONGUE;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_TONGUE_SQUINT:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_TONGUE_SQUINT;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_TONGUE_WINK:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_TONGUE_WINK;
                    break;

                case FontAwesomeIconType.SOLID_GRIN_WINK:
                    unicode = FontAwesomeUnicode.SOLID_GRIN_WINK;
                    break;

                case FontAwesomeIconType.SOLID_GRIP_HORIZONTAL:
                    unicode = FontAwesomeUnicode.SOLID_GRIP_HORIZONTAL;
                    break;

                case FontAwesomeIconType.SOLID_GRIP_VERTICAL:
                    unicode = FontAwesomeUnicode.SOLID_GRIP_VERTICAL;
                    break;

                case FontAwesomeIconType.SOLID_H_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_H_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_HAMMER:
                    unicode = FontAwesomeUnicode.SOLID_HAMMER;
                    break;

                case FontAwesomeIconType.SOLID_HAMSA:
                    unicode = FontAwesomeUnicode.SOLID_HAMSA;
                    break;

                case FontAwesomeIconType.SOLID_HAND_HOLDING:
                    unicode = FontAwesomeUnicode.SOLID_HAND_HOLDING;
                    break;

                case FontAwesomeIconType.SOLID_HAND_HOLDING_HEART:
                    unicode = FontAwesomeUnicode.SOLID_HAND_HOLDING_HEART;
                    break;

                case FontAwesomeIconType.SOLID_HAND_HOLDING_USD:
                    unicode = FontAwesomeUnicode.SOLID_HAND_HOLDING_USD;
                    break;

                case FontAwesomeIconType.SOLID_HAND_LIZARD:
                    unicode = FontAwesomeUnicode.SOLID_HAND_LIZARD;
                    break;

                case FontAwesomeIconType.SOLID_HAND_PAPER:
                    unicode = FontAwesomeUnicode.SOLID_HAND_PAPER;
                    break;

                case FontAwesomeIconType.SOLID_HAND_PEACE:
                    unicode = FontAwesomeUnicode.SOLID_HAND_PEACE;
                    break;

                case FontAwesomeIconType.SOLID_HAND_POINT_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_HAND_POINT_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_HAND_POINT_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_HAND_POINT_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_HAND_POINT_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_HAND_POINT_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_HAND_POINT_UP:
                    unicode = FontAwesomeUnicode.SOLID_HAND_POINT_UP;
                    break;

                case FontAwesomeIconType.SOLID_HAND_POINTER:
                    unicode = FontAwesomeUnicode.SOLID_HAND_POINTER;
                    break;

                case FontAwesomeIconType.SOLID_HAND_ROCK:
                    unicode = FontAwesomeUnicode.SOLID_HAND_ROCK;
                    break;

                case FontAwesomeIconType.SOLID_HAND_SCISSORS:
                    unicode = FontAwesomeUnicode.SOLID_HAND_SCISSORS;
                    break;

                case FontAwesomeIconType.SOLID_HAND_SPOCK:
                    unicode = FontAwesomeUnicode.SOLID_HAND_SPOCK;
                    break;

                case FontAwesomeIconType.SOLID_HANDS:
                    unicode = FontAwesomeUnicode.SOLID_HANDS;
                    break;

                case FontAwesomeIconType.SOLID_HANDS_HELPING:
                    unicode = FontAwesomeUnicode.SOLID_HANDS_HELPING;
                    break;

                case FontAwesomeIconType.SOLID_HANDSHAKE:
                    unicode = FontAwesomeUnicode.SOLID_HANDSHAKE;
                    break;

                case FontAwesomeIconType.SOLID_HANUKIAH:
                    unicode = FontAwesomeUnicode.SOLID_HANUKIAH;
                    break;

                case FontAwesomeIconType.SOLID_HASHTAG:
                    unicode = FontAwesomeUnicode.SOLID_HASHTAG;
                    break;

                case FontAwesomeIconType.SOLID_HAT_WIZARD:
                    unicode = FontAwesomeUnicode.SOLID_HAT_WIZARD;
                    break;

                case FontAwesomeIconType.SOLID_HAYKAL:
                    unicode = FontAwesomeUnicode.SOLID_HAYKAL;
                    break;

                case FontAwesomeIconType.SOLID_HDD:
                    unicode = FontAwesomeUnicode.SOLID_HDD;
                    break;

                case FontAwesomeIconType.SOLID_HEADING:
                    unicode = FontAwesomeUnicode.SOLID_HEADING;
                    break;

                case FontAwesomeIconType.SOLID_HEADPHONES:
                    unicode = FontAwesomeUnicode.SOLID_HEADPHONES;
                    break;

                case FontAwesomeIconType.SOLID_HEADPHONES_ALT:
                    unicode = FontAwesomeUnicode.SOLID_HEADPHONES_ALT;
                    break;

                case FontAwesomeIconType.SOLID_HEADSET:
                    unicode = FontAwesomeUnicode.SOLID_HEADSET;
                    break;

                case FontAwesomeIconType.SOLID_HEART:
                    unicode = FontAwesomeUnicode.SOLID_HEART;
                    break;

                case FontAwesomeIconType.SOLID_HEARTBEAT:
                    unicode = FontAwesomeUnicode.SOLID_HEARTBEAT;
                    break;

                case FontAwesomeIconType.SOLID_HELICOPTER:
                    unicode = FontAwesomeUnicode.SOLID_HELICOPTER;
                    break;

                case FontAwesomeIconType.SOLID_HIGHLIGHTER:
                    unicode = FontAwesomeUnicode.SOLID_HIGHLIGHTER;
                    break;

                case FontAwesomeIconType.SOLID_HIKING:
                    unicode = FontAwesomeUnicode.SOLID_HIKING;
                    break;

                case FontAwesomeIconType.SOLID_HIPPO:
                    unicode = FontAwesomeUnicode.SOLID_HIPPO;
                    break;

                case FontAwesomeIconType.SOLID_HISTORY:
                    unicode = FontAwesomeUnicode.SOLID_HISTORY;
                    break;

                case FontAwesomeIconType.SOLID_HOCKEY_PUCK:
                    unicode = FontAwesomeUnicode.SOLID_HOCKEY_PUCK;
                    break;

                case FontAwesomeIconType.SOLID_HOME:
                    unicode = FontAwesomeUnicode.SOLID_HOME;
                    break;

                case FontAwesomeIconType.SOLID_HORSE:
                    unicode = FontAwesomeUnicode.SOLID_HORSE;
                    break;

                case FontAwesomeIconType.SOLID_HOSPITAL:
                    unicode = FontAwesomeUnicode.SOLID_HOSPITAL;
                    break;

                case FontAwesomeIconType.SOLID_HOSPITAL_ALT:
                    unicode = FontAwesomeUnicode.SOLID_HOSPITAL_ALT;
                    break;

                case FontAwesomeIconType.SOLID_HOSPITAL_SYMBOL:
                    unicode = FontAwesomeUnicode.SOLID_HOSPITAL_SYMBOL;
                    break;

                case FontAwesomeIconType.SOLID_HOT_TUB:
                    unicode = FontAwesomeUnicode.SOLID_HOT_TUB;
                    break;

                case FontAwesomeIconType.SOLID_HOTEL:
                    unicode = FontAwesomeUnicode.SOLID_HOTEL;
                    break;

                case FontAwesomeIconType.SOLID_HOURGLASS:
                    unicode = FontAwesomeUnicode.SOLID_HOURGLASS;
                    break;

                case FontAwesomeIconType.SOLID_HOURGLASS_END:
                    unicode = FontAwesomeUnicode.SOLID_HOURGLASS_END;
                    break;

                case FontAwesomeIconType.SOLID_HOURGLASS_HALF:
                    unicode = FontAwesomeUnicode.SOLID_HOURGLASS_HALF;
                    break;

                case FontAwesomeIconType.SOLID_HOURGLASS_START:
                    unicode = FontAwesomeUnicode.SOLID_HOURGLASS_START;
                    break;

                case FontAwesomeIconType.SOLID_HOUSE_DAMAGE:
                    unicode = FontAwesomeUnicode.SOLID_HOUSE_DAMAGE;
                    break;

                case FontAwesomeIconType.SOLID_HRYVNIA:
                    unicode = FontAwesomeUnicode.SOLID_HRYVNIA;
                    break;

                case FontAwesomeIconType.SOLID_I_CURSOR:
                    unicode = FontAwesomeUnicode.SOLID_I_CURSOR;
                    break;

                case FontAwesomeIconType.SOLID_ID_BADGE:
                    unicode = FontAwesomeUnicode.SOLID_ID_BADGE;
                    break;

                case FontAwesomeIconType.SOLID_ID_CARD:
                    unicode = FontAwesomeUnicode.SOLID_ID_CARD;
                    break;

                case FontAwesomeIconType.SOLID_ID_CARD_ALT:
                    unicode = FontAwesomeUnicode.SOLID_ID_CARD_ALT;
                    break;

                case FontAwesomeIconType.SOLID_IMAGE:
                    unicode = FontAwesomeUnicode.SOLID_IMAGE;
                    break;

                case FontAwesomeIconType.SOLID_IMAGES:
                    unicode = FontAwesomeUnicode.SOLID_IMAGES;
                    break;

                case FontAwesomeIconType.SOLID_INBOX:
                    unicode = FontAwesomeUnicode.SOLID_INBOX;
                    break;

                case FontAwesomeIconType.SOLID_INDENT:
                    unicode = FontAwesomeUnicode.SOLID_INDENT;
                    break;

                case FontAwesomeIconType.SOLID_INDUSTRY:
                    unicode = FontAwesomeUnicode.SOLID_INDUSTRY;
                    break;

                case FontAwesomeIconType.SOLID_INFINITY:
                    unicode = FontAwesomeUnicode.SOLID_INFINITY;
                    break;

                case FontAwesomeIconType.SOLID_INFO:
                    unicode = FontAwesomeUnicode.SOLID_INFO;
                    break;

                case FontAwesomeIconType.SOLID_INFO_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_INFO_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_ITALIC:
                    unicode = FontAwesomeUnicode.SOLID_ITALIC;
                    break;

                case FontAwesomeIconType.SOLID_JEDI:
                    unicode = FontAwesomeUnicode.SOLID_JEDI;
                    break;

                case FontAwesomeIconType.SOLID_JOINT:
                    unicode = FontAwesomeUnicode.SOLID_JOINT;
                    break;

                case FontAwesomeIconType.SOLID_JOURNAL_WHILLS:
                    unicode = FontAwesomeUnicode.SOLID_JOURNAL_WHILLS;
                    break;

                case FontAwesomeIconType.SOLID_KAABA:
                    unicode = FontAwesomeUnicode.SOLID_KAABA;
                    break;

                case FontAwesomeIconType.SOLID_KEY:
                    unicode = FontAwesomeUnicode.SOLID_KEY;
                    break;

                case FontAwesomeIconType.SOLID_KEYBOARD:
                    unicode = FontAwesomeUnicode.SOLID_KEYBOARD;
                    break;

                case FontAwesomeIconType.SOLID_KHANDA:
                    unicode = FontAwesomeUnicode.SOLID_KHANDA;
                    break;

                case FontAwesomeIconType.SOLID_KISS:
                    unicode = FontAwesomeUnicode.SOLID_KISS;
                    break;

                case FontAwesomeIconType.SOLID_KISS_BEAM:
                    unicode = FontAwesomeUnicode.SOLID_KISS_BEAM;
                    break;

                case FontAwesomeIconType.SOLID_KISS_WINK_HEART:
                    unicode = FontAwesomeUnicode.SOLID_KISS_WINK_HEART;
                    break;

                case FontAwesomeIconType.SOLID_KIWI_BIRD:
                    unicode = FontAwesomeUnicode.SOLID_KIWI_BIRD;
                    break;

                case FontAwesomeIconType.SOLID_LANDMARK:
                    unicode = FontAwesomeUnicode.SOLID_LANDMARK;
                    break;

                case FontAwesomeIconType.SOLID_LANGUAGE:
                    unicode = FontAwesomeUnicode.SOLID_LANGUAGE;
                    break;

                case FontAwesomeIconType.SOLID_LAPTOP:
                    unicode = FontAwesomeUnicode.SOLID_LAPTOP;
                    break;

                case FontAwesomeIconType.SOLID_LAPTOP_CODE:
                    unicode = FontAwesomeUnicode.SOLID_LAPTOP_CODE;
                    break;

                case FontAwesomeIconType.SOLID_LAUGH:
                    unicode = FontAwesomeUnicode.SOLID_LAUGH;
                    break;

                case FontAwesomeIconType.SOLID_LAUGH_BEAM:
                    unicode = FontAwesomeUnicode.SOLID_LAUGH_BEAM;
                    break;

                case FontAwesomeIconType.SOLID_LAUGH_SQUINT:
                    unicode = FontAwesomeUnicode.SOLID_LAUGH_SQUINT;
                    break;

                case FontAwesomeIconType.SOLID_LAUGH_WINK:
                    unicode = FontAwesomeUnicode.SOLID_LAUGH_WINK;
                    break;

                case FontAwesomeIconType.SOLID_LAYER_GROUP:
                    unicode = FontAwesomeUnicode.SOLID_LAYER_GROUP;
                    break;

                case FontAwesomeIconType.SOLID_LEAF:
                    unicode = FontAwesomeUnicode.SOLID_LEAF;
                    break;

                case FontAwesomeIconType.SOLID_LEMON:
                    unicode = FontAwesomeUnicode.SOLID_LEMON;
                    break;

                case FontAwesomeIconType.SOLID_LESS_THAN:
                    unicode = FontAwesomeUnicode.SOLID_LESS_THAN;
                    break;

                case FontAwesomeIconType.SOLID_LESS_THAN_EQUAL:
                    unicode = FontAwesomeUnicode.SOLID_LESS_THAN_EQUAL;
                    break;

                case FontAwesomeIconType.SOLID_LEVEL_DOWN_ALT:
                    unicode = FontAwesomeUnicode.SOLID_LEVEL_DOWN_ALT;
                    break;

                case FontAwesomeIconType.SOLID_LEVEL_UP_ALT:
                    unicode = FontAwesomeUnicode.SOLID_LEVEL_UP_ALT;
                    break;

                case FontAwesomeIconType.SOLID_LIFE_RING:
                    unicode = FontAwesomeUnicode.SOLID_LIFE_RING;
                    break;

                case FontAwesomeIconType.SOLID_LIGHTBULB:
                    unicode = FontAwesomeUnicode.SOLID_LIGHTBULB;
                    break;

                case FontAwesomeIconType.SOLID_LINK:
                    unicode = FontAwesomeUnicode.SOLID_LINK;
                    break;

                case FontAwesomeIconType.SOLID_LIRA_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_LIRA_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_LIST:
                    unicode = FontAwesomeUnicode.SOLID_LIST;
                    break;

                case FontAwesomeIconType.SOLID_LIST_ALT:
                    unicode = FontAwesomeUnicode.SOLID_LIST_ALT;
                    break;

                case FontAwesomeIconType.SOLID_LIST_OL:
                    unicode = FontAwesomeUnicode.SOLID_LIST_OL;
                    break;

                case FontAwesomeIconType.SOLID_LIST_UL:
                    unicode = FontAwesomeUnicode.SOLID_LIST_UL;
                    break;

                case FontAwesomeIconType.SOLID_LOCATION_ARROW:
                    unicode = FontAwesomeUnicode.SOLID_LOCATION_ARROW;
                    break;

                case FontAwesomeIconType.SOLID_LOCK:
                    unicode = FontAwesomeUnicode.SOLID_LOCK;
                    break;

                case FontAwesomeIconType.SOLID_LOCK_OPEN:
                    unicode = FontAwesomeUnicode.SOLID_LOCK_OPEN;
                    break;

                case FontAwesomeIconType.SOLID_LONG_ARROW_ALT_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_LONG_ARROW_ALT_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_LONG_ARROW_ALT_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_LONG_ARROW_ALT_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_LONG_ARROW_ALT_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_LONG_ARROW_ALT_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_LONG_ARROW_ALT_UP:
                    unicode = FontAwesomeUnicode.SOLID_LONG_ARROW_ALT_UP;
                    break;

                case FontAwesomeIconType.SOLID_LOW_VISION:
                    unicode = FontAwesomeUnicode.SOLID_LOW_VISION;
                    break;

                case FontAwesomeIconType.SOLID_LUGGAGE_CART:
                    unicode = FontAwesomeUnicode.SOLID_LUGGAGE_CART;
                    break;

                case FontAwesomeIconType.SOLID_MAGIC:
                    unicode = FontAwesomeUnicode.SOLID_MAGIC;
                    break;

                case FontAwesomeIconType.SOLID_MAGNET:
                    unicode = FontAwesomeUnicode.SOLID_MAGNET;
                    break;

                case FontAwesomeIconType.SOLID_MAIL_BULK:
                    unicode = FontAwesomeUnicode.SOLID_MAIL_BULK;
                    break;

                case FontAwesomeIconType.SOLID_MALE:
                    unicode = FontAwesomeUnicode.SOLID_MALE;
                    break;

                case FontAwesomeIconType.SOLID_MAP:
                    unicode = FontAwesomeUnicode.SOLID_MAP;
                    break;

                case FontAwesomeIconType.SOLID_MAP_MARKED:
                    unicode = FontAwesomeUnicode.SOLID_MAP_MARKED;
                    break;

                case FontAwesomeIconType.SOLID_MAP_MARKED_ALT:
                    unicode = FontAwesomeUnicode.SOLID_MAP_MARKED_ALT;
                    break;

                case FontAwesomeIconType.SOLID_MAP_MARKER:
                    unicode = FontAwesomeUnicode.SOLID_MAP_MARKER;
                    break;

                case FontAwesomeIconType.SOLID_MAP_MARKER_ALT:
                    unicode = FontAwesomeUnicode.SOLID_MAP_MARKER_ALT;
                    break;

                case FontAwesomeIconType.SOLID_MAP_PIN:
                    unicode = FontAwesomeUnicode.SOLID_MAP_PIN;
                    break;

                case FontAwesomeIconType.SOLID_MAP_SIGNS:
                    unicode = FontAwesomeUnicode.SOLID_MAP_SIGNS;
                    break;

                case FontAwesomeIconType.SOLID_MARKER:
                    unicode = FontAwesomeUnicode.SOLID_MARKER;
                    break;

                case FontAwesomeIconType.SOLID_MARS:
                    unicode = FontAwesomeUnicode.SOLID_MARS;
                    break;

                case FontAwesomeIconType.SOLID_MARS_DOUBLE:
                    unicode = FontAwesomeUnicode.SOLID_MARS_DOUBLE;
                    break;

                case FontAwesomeIconType.SOLID_MARS_STROKE:
                    unicode = FontAwesomeUnicode.SOLID_MARS_STROKE;
                    break;

                case FontAwesomeIconType.SOLID_MARS_STROKE_H:
                    unicode = FontAwesomeUnicode.SOLID_MARS_STROKE_H;
                    break;

                case FontAwesomeIconType.SOLID_MARS_STROKE_V:
                    unicode = FontAwesomeUnicode.SOLID_MARS_STROKE_V;
                    break;

                case FontAwesomeIconType.SOLID_MASK:
                    unicode = FontAwesomeUnicode.SOLID_MASK;
                    break;

                case FontAwesomeIconType.SOLID_MEDAL:
                    unicode = FontAwesomeUnicode.SOLID_MEDAL;
                    break;

                case FontAwesomeIconType.SOLID_MEDKIT:
                    unicode = FontAwesomeUnicode.SOLID_MEDKIT;
                    break;

                case FontAwesomeIconType.SOLID_MEH:
                    unicode = FontAwesomeUnicode.SOLID_MEH;
                    break;

                case FontAwesomeIconType.SOLID_MEH_BLANK:
                    unicode = FontAwesomeUnicode.SOLID_MEH_BLANK;
                    break;

                case FontAwesomeIconType.SOLID_MEH_ROLLING_EYES:
                    unicode = FontAwesomeUnicode.SOLID_MEH_ROLLING_EYES;
                    break;

                case FontAwesomeIconType.SOLID_MEMORY:
                    unicode = FontAwesomeUnicode.SOLID_MEMORY;
                    break;

                case FontAwesomeIconType.SOLID_MENORAH:
                    unicode = FontAwesomeUnicode.SOLID_MENORAH;
                    break;

                case FontAwesomeIconType.SOLID_MERCURY:
                    unicode = FontAwesomeUnicode.SOLID_MERCURY;
                    break;

                case FontAwesomeIconType.SOLID_METEOR:
                    unicode = FontAwesomeUnicode.SOLID_METEOR;
                    break;

                case FontAwesomeIconType.SOLID_MICROCHIP:
                    unicode = FontAwesomeUnicode.SOLID_MICROCHIP;
                    break;

                case FontAwesomeIconType.SOLID_MICROPHONE:
                    unicode = FontAwesomeUnicode.SOLID_MICROPHONE;
                    break;

                case FontAwesomeIconType.SOLID_MICROPHONE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_MICROPHONE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_MICROPHONE_ALT_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_MICROPHONE_ALT_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_MICROPHONE_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_MICROPHONE_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_MICROSCOPE:
                    unicode = FontAwesomeUnicode.SOLID_MICROSCOPE;
                    break;

                case FontAwesomeIconType.SOLID_MINUS:
                    unicode = FontAwesomeUnicode.SOLID_MINUS;
                    break;

                case FontAwesomeIconType.SOLID_MINUS_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_MINUS_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_MINUS_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_MINUS_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_MOBILE:
                    unicode = FontAwesomeUnicode.SOLID_MOBILE;
                    break;

                case FontAwesomeIconType.SOLID_MOBILE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_MOBILE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_MONEY_BILL:
                    unicode = FontAwesomeUnicode.SOLID_MONEY_BILL;
                    break;

                case FontAwesomeIconType.SOLID_MONEY_BILL_ALT:
                    unicode = FontAwesomeUnicode.SOLID_MONEY_BILL_ALT;
                    break;

                case FontAwesomeIconType.SOLID_MONEY_BILL_WAVE:
                    unicode = FontAwesomeUnicode.SOLID_MONEY_BILL_WAVE;
                    break;

                case FontAwesomeIconType.SOLID_MONEY_BILL_WAVE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_MONEY_BILL_WAVE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_MONEY_CHECK:
                    unicode = FontAwesomeUnicode.SOLID_MONEY_CHECK;
                    break;

                case FontAwesomeIconType.SOLID_MONEY_CHECK_ALT:
                    unicode = FontAwesomeUnicode.SOLID_MONEY_CHECK_ALT;
                    break;

                case FontAwesomeIconType.SOLID_MONUMENT:
                    unicode = FontAwesomeUnicode.SOLID_MONUMENT;
                    break;

                case FontAwesomeIconType.SOLID_MOON:
                    unicode = FontAwesomeUnicode.SOLID_MOON;
                    break;

                case FontAwesomeIconType.SOLID_MORTAR_PESTLE:
                    unicode = FontAwesomeUnicode.SOLID_MORTAR_PESTLE;
                    break;

                case FontAwesomeIconType.SOLID_MOSQUE:
                    unicode = FontAwesomeUnicode.SOLID_MOSQUE;
                    break;

                case FontAwesomeIconType.SOLID_MOTORCYCLE:
                    unicode = FontAwesomeUnicode.SOLID_MOTORCYCLE;
                    break;

                case FontAwesomeIconType.SOLID_MOUNTAIN:
                    unicode = FontAwesomeUnicode.SOLID_MOUNTAIN;
                    break;

                case FontAwesomeIconType.SOLID_MOUSE_POINTER:
                    unicode = FontAwesomeUnicode.SOLID_MOUSE_POINTER;
                    break;

                case FontAwesomeIconType.SOLID_MUSIC:
                    unicode = FontAwesomeUnicode.SOLID_MUSIC;
                    break;

                case FontAwesomeIconType.SOLID_NETWORK_WIRED:
                    unicode = FontAwesomeUnicode.SOLID_NETWORK_WIRED;
                    break;

                case FontAwesomeIconType.SOLID_NEUTER:
                    unicode = FontAwesomeUnicode.SOLID_NEUTER;
                    break;

                case FontAwesomeIconType.SOLID_NEWSPAPER:
                    unicode = FontAwesomeUnicode.SOLID_NEWSPAPER;
                    break;

                case FontAwesomeIconType.SOLID_NOT_EQUAL:
                    unicode = FontAwesomeUnicode.SOLID_NOT_EQUAL;
                    break;

                case FontAwesomeIconType.SOLID_NOTES_MEDICAL:
                    unicode = FontAwesomeUnicode.SOLID_NOTES_MEDICAL;
                    break;

                case FontAwesomeIconType.SOLID_OBJECT_GROUP:
                    unicode = FontAwesomeUnicode.SOLID_OBJECT_GROUP;
                    break;

                case FontAwesomeIconType.SOLID_OBJECT_UNGROUP:
                    unicode = FontAwesomeUnicode.SOLID_OBJECT_UNGROUP;
                    break;

                case FontAwesomeIconType.SOLID_OIL_CAN:
                    unicode = FontAwesomeUnicode.SOLID_OIL_CAN;
                    break;

                case FontAwesomeIconType.SOLID_OM:
                    unicode = FontAwesomeUnicode.SOLID_OM;
                    break;

                case FontAwesomeIconType.SOLID_OTTER:
                    unicode = FontAwesomeUnicode.SOLID_OTTER;
                    break;

                case FontAwesomeIconType.SOLID_OUTDENT:
                    unicode = FontAwesomeUnicode.SOLID_OUTDENT;
                    break;

                case FontAwesomeIconType.SOLID_PAINT_BRUSH:
                    unicode = FontAwesomeUnicode.SOLID_PAINT_BRUSH;
                    break;

                case FontAwesomeIconType.SOLID_PAINT_ROLLER:
                    unicode = FontAwesomeUnicode.SOLID_PAINT_ROLLER;
                    break;

                case FontAwesomeIconType.SOLID_PALETTE:
                    unicode = FontAwesomeUnicode.SOLID_PALETTE;
                    break;

                case FontAwesomeIconType.SOLID_PALLET:
                    unicode = FontAwesomeUnicode.SOLID_PALLET;
                    break;

                case FontAwesomeIconType.SOLID_PAPER_PLANE:
                    unicode = FontAwesomeUnicode.SOLID_PAPER_PLANE;
                    break;

                case FontAwesomeIconType.SOLID_PAPERCLIP:
                    unicode = FontAwesomeUnicode.SOLID_PAPERCLIP;
                    break;

                case FontAwesomeIconType.SOLID_PARACHUTE_BOX:
                    unicode = FontAwesomeUnicode.SOLID_PARACHUTE_BOX;
                    break;

                case FontAwesomeIconType.SOLID_PARAGRAPH:
                    unicode = FontAwesomeUnicode.SOLID_PARAGRAPH;
                    break;

                case FontAwesomeIconType.SOLID_PARKING:
                    unicode = FontAwesomeUnicode.SOLID_PARKING;
                    break;

                case FontAwesomeIconType.SOLID_PASSPORT:
                    unicode = FontAwesomeUnicode.SOLID_PASSPORT;
                    break;

                case FontAwesomeIconType.SOLID_PASTAFARIANISM:
                    unicode = FontAwesomeUnicode.SOLID_PASTAFARIANISM;
                    break;

                case FontAwesomeIconType.SOLID_PASTE:
                    unicode = FontAwesomeUnicode.SOLID_PASTE;
                    break;

                case FontAwesomeIconType.SOLID_PAUSE:
                    unicode = FontAwesomeUnicode.SOLID_PAUSE;
                    break;

                case FontAwesomeIconType.SOLID_PAUSE_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_PAUSE_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_PAW:
                    unicode = FontAwesomeUnicode.SOLID_PAW;
                    break;

                case FontAwesomeIconType.SOLID_PEACE:
                    unicode = FontAwesomeUnicode.SOLID_PEACE;
                    break;

                case FontAwesomeIconType.SOLID_PEN:
                    unicode = FontAwesomeUnicode.SOLID_PEN;
                    break;

                case FontAwesomeIconType.SOLID_PEN_ALT:
                    unicode = FontAwesomeUnicode.SOLID_PEN_ALT;
                    break;

                case FontAwesomeIconType.SOLID_PEN_FANCY:
                    unicode = FontAwesomeUnicode.SOLID_PEN_FANCY;
                    break;

                case FontAwesomeIconType.SOLID_PEN_NIB:
                    unicode = FontAwesomeUnicode.SOLID_PEN_NIB;
                    break;

                case FontAwesomeIconType.SOLID_PEN_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_PEN_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_PENCIL_ALT:
                    unicode = FontAwesomeUnicode.SOLID_PENCIL_ALT;
                    break;

                case FontAwesomeIconType.SOLID_PENCIL_RULER:
                    unicode = FontAwesomeUnicode.SOLID_PENCIL_RULER;
                    break;

                case FontAwesomeIconType.SOLID_PEOPLE_CARRY:
                    unicode = FontAwesomeUnicode.SOLID_PEOPLE_CARRY;
                    break;

                case FontAwesomeIconType.SOLID_PERCENT:
                    unicode = FontAwesomeUnicode.SOLID_PERCENT;
                    break;

                case FontAwesomeIconType.SOLID_PERCENTAGE:
                    unicode = FontAwesomeUnicode.SOLID_PERCENTAGE;
                    break;

                case FontAwesomeIconType.SOLID_PERSON_BOOTH:
                    unicode = FontAwesomeUnicode.SOLID_PERSON_BOOTH;
                    break;

                case FontAwesomeIconType.SOLID_PHONE:
                    unicode = FontAwesomeUnicode.SOLID_PHONE;
                    break;

                case FontAwesomeIconType.SOLID_PHONE_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_PHONE_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_PHONE_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_PHONE_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_PHONE_VOLUME:
                    unicode = FontAwesomeUnicode.SOLID_PHONE_VOLUME;
                    break;

                case FontAwesomeIconType.SOLID_PIGGY_BANK:
                    unicode = FontAwesomeUnicode.SOLID_PIGGY_BANK;
                    break;

                case FontAwesomeIconType.SOLID_PILLS:
                    unicode = FontAwesomeUnicode.SOLID_PILLS;
                    break;

                case FontAwesomeIconType.SOLID_PLACE_OF_WORSHIP:
                    unicode = FontAwesomeUnicode.SOLID_PLACE_OF_WORSHIP;
                    break;

                case FontAwesomeIconType.SOLID_PLANE:
                    unicode = FontAwesomeUnicode.SOLID_PLANE;
                    break;

                case FontAwesomeIconType.SOLID_PLANE_ARRIVAL:
                    unicode = FontAwesomeUnicode.SOLID_PLANE_ARRIVAL;
                    break;

                case FontAwesomeIconType.SOLID_PLANE_DEPARTURE:
                    unicode = FontAwesomeUnicode.SOLID_PLANE_DEPARTURE;
                    break;

                case FontAwesomeIconType.SOLID_PLAY:
                    unicode = FontAwesomeUnicode.SOLID_PLAY;
                    break;

                case FontAwesomeIconType.SOLID_PLAY_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_PLAY_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_PLUG:
                    unicode = FontAwesomeUnicode.SOLID_PLUG;
                    break;

                case FontAwesomeIconType.SOLID_PLUS:
                    unicode = FontAwesomeUnicode.SOLID_PLUS;
                    break;

                case FontAwesomeIconType.SOLID_PLUS_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_PLUS_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_PLUS_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_PLUS_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_PODCAST:
                    unicode = FontAwesomeUnicode.SOLID_PODCAST;
                    break;

                case FontAwesomeIconType.SOLID_POLL:
                    unicode = FontAwesomeUnicode.SOLID_POLL;
                    break;

                case FontAwesomeIconType.SOLID_POLL_H:
                    unicode = FontAwesomeUnicode.SOLID_POLL_H;
                    break;

                case FontAwesomeIconType.SOLID_POO:
                    unicode = FontAwesomeUnicode.SOLID_POO;
                    break;

                case FontAwesomeIconType.SOLID_POO_STORM:
                    unicode = FontAwesomeUnicode.SOLID_POO_STORM;
                    break;

                case FontAwesomeIconType.SOLID_POOP:
                    unicode = FontAwesomeUnicode.SOLID_POOP;
                    break;

                case FontAwesomeIconType.SOLID_PORTRAIT:
                    unicode = FontAwesomeUnicode.SOLID_PORTRAIT;
                    break;

                case FontAwesomeIconType.SOLID_POUND_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_POUND_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_POWER_OFF:
                    unicode = FontAwesomeUnicode.SOLID_POWER_OFF;
                    break;

                case FontAwesomeIconType.SOLID_PRAY:
                    unicode = FontAwesomeUnicode.SOLID_PRAY;
                    break;

                case FontAwesomeIconType.SOLID_PRAYING_HANDS:
                    unicode = FontAwesomeUnicode.SOLID_PRAYING_HANDS;
                    break;

                case FontAwesomeIconType.SOLID_PRESCRIPTION:
                    unicode = FontAwesomeUnicode.SOLID_PRESCRIPTION;
                    break;

                case FontAwesomeIconType.SOLID_PRESCRIPTION_BOTTLE:
                    unicode = FontAwesomeUnicode.SOLID_PRESCRIPTION_BOTTLE;
                    break;

                case FontAwesomeIconType.SOLID_PRESCRIPTION_BOTTLE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_PRESCRIPTION_BOTTLE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_PRINT:
                    unicode = FontAwesomeUnicode.SOLID_PRINT;
                    break;

                case FontAwesomeIconType.SOLID_PROCEDURES:
                    unicode = FontAwesomeUnicode.SOLID_PROCEDURES;
                    break;

                case FontAwesomeIconType.SOLID_PROJECT_DIAGRAM:
                    unicode = FontAwesomeUnicode.SOLID_PROJECT_DIAGRAM;
                    break;

                case FontAwesomeIconType.SOLID_PUZZLE_PIECE:
                    unicode = FontAwesomeUnicode.SOLID_PUZZLE_PIECE;
                    break;

                case FontAwesomeIconType.SOLID_QRCODE:
                    unicode = FontAwesomeUnicode.SOLID_QRCODE;
                    break;

                case FontAwesomeIconType.SOLID_QUESTION:
                    unicode = FontAwesomeUnicode.SOLID_QUESTION;
                    break;

                case FontAwesomeIconType.SOLID_QUESTION_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_QUESTION_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_QUIDDITCH:
                    unicode = FontAwesomeUnicode.SOLID_QUIDDITCH;
                    break;

                case FontAwesomeIconType.SOLID_QUOTE_LEFT:
                    unicode = FontAwesomeUnicode.SOLID_QUOTE_LEFT;
                    break;

                case FontAwesomeIconType.SOLID_QUOTE_RIGHT:
                    unicode = FontAwesomeUnicode.SOLID_QUOTE_RIGHT;
                    break;

                case FontAwesomeIconType.SOLID_QURAN:
                    unicode = FontAwesomeUnicode.SOLID_QURAN;
                    break;

                case FontAwesomeIconType.SOLID_RAINBOW:
                    unicode = FontAwesomeUnicode.SOLID_RAINBOW;
                    break;

                case FontAwesomeIconType.SOLID_RANDOM:
                    unicode = FontAwesomeUnicode.SOLID_RANDOM;
                    break;

                case FontAwesomeIconType.SOLID_RECEIPT:
                    unicode = FontAwesomeUnicode.SOLID_RECEIPT;
                    break;

                case FontAwesomeIconType.SOLID_RECYCLE:
                    unicode = FontAwesomeUnicode.SOLID_RECYCLE;
                    break;

                case FontAwesomeIconType.SOLID_REDO:
                    unicode = FontAwesomeUnicode.SOLID_REDO;
                    break;

                case FontAwesomeIconType.SOLID_REDO_ALT:
                    unicode = FontAwesomeUnicode.SOLID_REDO_ALT;
                    break;

                case FontAwesomeIconType.SOLID_REGISTERED:
                    unicode = FontAwesomeUnicode.SOLID_REGISTERED;
                    break;

                case FontAwesomeIconType.SOLID_REPLY:
                    unicode = FontAwesomeUnicode.SOLID_REPLY;
                    break;

                case FontAwesomeIconType.SOLID_REPLY_ALL:
                    unicode = FontAwesomeUnicode.SOLID_REPLY_ALL;
                    break;

                case FontAwesomeIconType.SOLID_REPUBLICAN:
                    unicode = FontAwesomeUnicode.SOLID_REPUBLICAN;
                    break;

                case FontAwesomeIconType.SOLID_RETWEET:
                    unicode = FontAwesomeUnicode.SOLID_RETWEET;
                    break;

                case FontAwesomeIconType.SOLID_RIBBON:
                    unicode = FontAwesomeUnicode.SOLID_RIBBON;
                    break;

                case FontAwesomeIconType.SOLID_RING:
                    unicode = FontAwesomeUnicode.SOLID_RING;
                    break;

                case FontAwesomeIconType.SOLID_ROAD:
                    unicode = FontAwesomeUnicode.SOLID_ROAD;
                    break;

                case FontAwesomeIconType.SOLID_ROBOT:
                    unicode = FontAwesomeUnicode.SOLID_ROBOT;
                    break;

                case FontAwesomeIconType.SOLID_ROCKET:
                    unicode = FontAwesomeUnicode.SOLID_ROCKET;
                    break;

                case FontAwesomeIconType.SOLID_ROUTE:
                    unicode = FontAwesomeUnicode.SOLID_ROUTE;
                    break;

                case FontAwesomeIconType.SOLID_RSS:
                    unicode = FontAwesomeUnicode.SOLID_RSS;
                    break;

                case FontAwesomeIconType.SOLID_RSS_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_RSS_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_RUBLE_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_RUBLE_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_RULER:
                    unicode = FontAwesomeUnicode.SOLID_RULER;
                    break;

                case FontAwesomeIconType.SOLID_RULER_COMBINED:
                    unicode = FontAwesomeUnicode.SOLID_RULER_COMBINED;
                    break;

                case FontAwesomeIconType.SOLID_RULER_HORIZONTAL:
                    unicode = FontAwesomeUnicode.SOLID_RULER_HORIZONTAL;
                    break;

                case FontAwesomeIconType.SOLID_RULER_VERTICAL:
                    unicode = FontAwesomeUnicode.SOLID_RULER_VERTICAL;
                    break;

                case FontAwesomeIconType.SOLID_RUNNING:
                    unicode = FontAwesomeUnicode.SOLID_RUNNING;
                    break;

                case FontAwesomeIconType.SOLID_RUPEE_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_RUPEE_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_SAD_CRY:
                    unicode = FontAwesomeUnicode.SOLID_SAD_CRY;
                    break;

                case FontAwesomeIconType.SOLID_SAD_TEAR:
                    unicode = FontAwesomeUnicode.SOLID_SAD_TEAR;
                    break;

                case FontAwesomeIconType.SOLID_SAVE:
                    unicode = FontAwesomeUnicode.SOLID_SAVE;
                    break;

                case FontAwesomeIconType.SOLID_SCHOOL:
                    unicode = FontAwesomeUnicode.SOLID_SCHOOL;
                    break;

                case FontAwesomeIconType.SOLID_SCREWDRIVER:
                    unicode = FontAwesomeUnicode.SOLID_SCREWDRIVER;
                    break;

                case FontAwesomeIconType.SOLID_SCROLL:
                    unicode = FontAwesomeUnicode.SOLID_SCROLL;
                    break;

                case FontAwesomeIconType.SOLID_SEARCH:
                    unicode = FontAwesomeUnicode.SOLID_SEARCH;
                    break;

                case FontAwesomeIconType.SOLID_SEARCH_DOLLAR:
                    unicode = FontAwesomeUnicode.SOLID_SEARCH_DOLLAR;
                    break;

                case FontAwesomeIconType.SOLID_SEARCH_LOCATION:
                    unicode = FontAwesomeUnicode.SOLID_SEARCH_LOCATION;
                    break;

                case FontAwesomeIconType.SOLID_SEARCH_MINUS:
                    unicode = FontAwesomeUnicode.SOLID_SEARCH_MINUS;
                    break;

                case FontAwesomeIconType.SOLID_SEARCH_PLUS:
                    unicode = FontAwesomeUnicode.SOLID_SEARCH_PLUS;
                    break;

                case FontAwesomeIconType.SOLID_SEEDLING:
                    unicode = FontAwesomeUnicode.SOLID_SEEDLING;
                    break;

                case FontAwesomeIconType.SOLID_SERVER:
                    unicode = FontAwesomeUnicode.SOLID_SERVER;
                    break;

                case FontAwesomeIconType.SOLID_SHAPES:
                    unicode = FontAwesomeUnicode.SOLID_SHAPES;
                    break;

                case FontAwesomeIconType.SOLID_SHARE:
                    unicode = FontAwesomeUnicode.SOLID_SHARE;
                    break;

                case FontAwesomeIconType.SOLID_SHARE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_SHARE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_SHARE_ALT_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_SHARE_ALT_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_SHARE_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_SHARE_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_SHEKEL_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_SHEKEL_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_SHIELD_ALT:
                    unicode = FontAwesomeUnicode.SOLID_SHIELD_ALT;
                    break;

                case FontAwesomeIconType.SOLID_SHIP:
                    unicode = FontAwesomeUnicode.SOLID_SHIP;
                    break;

                case FontAwesomeIconType.SOLID_SHIPPING_FAST:
                    unicode = FontAwesomeUnicode.SOLID_SHIPPING_FAST;
                    break;

                case FontAwesomeIconType.SOLID_SHOE_PRINTS:
                    unicode = FontAwesomeUnicode.SOLID_SHOE_PRINTS;
                    break;

                case FontAwesomeIconType.SOLID_SHOPPING_BAG:
                    unicode = FontAwesomeUnicode.SOLID_SHOPPING_BAG;
                    break;

                case FontAwesomeIconType.SOLID_SHOPPING_BASKET:
                    unicode = FontAwesomeUnicode.SOLID_SHOPPING_BASKET;
                    break;

                case FontAwesomeIconType.SOLID_SHOPPING_CART:
                    unicode = FontAwesomeUnicode.SOLID_SHOPPING_CART;
                    break;

                case FontAwesomeIconType.SOLID_SHOWER:
                    unicode = FontAwesomeUnicode.SOLID_SHOWER;
                    break;

                case FontAwesomeIconType.SOLID_SHUTTLE_VAN:
                    unicode = FontAwesomeUnicode.SOLID_SHUTTLE_VAN;
                    break;

                case FontAwesomeIconType.SOLID_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_SIGN_IN_ALT:
                    unicode = FontAwesomeUnicode.SOLID_SIGN_IN_ALT;
                    break;

                case FontAwesomeIconType.SOLID_SIGN_LANGUAGE:
                    unicode = FontAwesomeUnicode.SOLID_SIGN_LANGUAGE;
                    break;

                case FontAwesomeIconType.SOLID_SIGN_OUT_ALT:
                    unicode = FontAwesomeUnicode.SOLID_SIGN_OUT_ALT;
                    break;

                case FontAwesomeIconType.SOLID_SIGNAL:
                    unicode = FontAwesomeUnicode.SOLID_SIGNAL;
                    break;

                case FontAwesomeIconType.SOLID_SIGNATURE:
                    unicode = FontAwesomeUnicode.SOLID_SIGNATURE;
                    break;

                case FontAwesomeIconType.SOLID_SITEMAP:
                    unicode = FontAwesomeUnicode.SOLID_SITEMAP;
                    break;

                case FontAwesomeIconType.SOLID_SKULL:
                    unicode = FontAwesomeUnicode.SOLID_SKULL;
                    break;

                case FontAwesomeIconType.SOLID_SKULL_CROSSBONES:
                    unicode = FontAwesomeUnicode.SOLID_SKULL_CROSSBONES;
                    break;

                case FontAwesomeIconType.SOLID_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_SLIDERS_H:
                    unicode = FontAwesomeUnicode.SOLID_SLIDERS_H;
                    break;

                case FontAwesomeIconType.SOLID_SMILE:
                    unicode = FontAwesomeUnicode.SOLID_SMILE;
                    break;

                case FontAwesomeIconType.SOLID_SMILE_BEAM:
                    unicode = FontAwesomeUnicode.SOLID_SMILE_BEAM;
                    break;

                case FontAwesomeIconType.SOLID_SMILE_WINK:
                    unicode = FontAwesomeUnicode.SOLID_SMILE_WINK;
                    break;

                case FontAwesomeIconType.SOLID_SMOG:
                    unicode = FontAwesomeUnicode.SOLID_SMOG;
                    break;

                case FontAwesomeIconType.SOLID_SMOKING:
                    unicode = FontAwesomeUnicode.SOLID_SMOKING;
                    break;

                case FontAwesomeIconType.SOLID_SMOKING_BAN:
                    unicode = FontAwesomeUnicode.SOLID_SMOKING_BAN;
                    break;

                case FontAwesomeIconType.SOLID_SNOWFLAKE:
                    unicode = FontAwesomeUnicode.SOLID_SNOWFLAKE;
                    break;

                case FontAwesomeIconType.SOLID_SOCKS:
                    unicode = FontAwesomeUnicode.SOLID_SOCKS;
                    break;

                case FontAwesomeIconType.SOLID_SOLAR_PANEL:
                    unicode = FontAwesomeUnicode.SOLID_SOLAR_PANEL;
                    break;

                case FontAwesomeIconType.SOLID_SORT:
                    unicode = FontAwesomeUnicode.SOLID_SORT;
                    break;

                case FontAwesomeIconType.SOLID_SORT_ALPHA_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_SORT_ALPHA_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_SORT_ALPHA_UP:
                    unicode = FontAwesomeUnicode.SOLID_SORT_ALPHA_UP;
                    break;

                case FontAwesomeIconType.SOLID_SORT_AMOUNT_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_SORT_AMOUNT_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_SORT_AMOUNT_UP:
                    unicode = FontAwesomeUnicode.SOLID_SORT_AMOUNT_UP;
                    break;

                case FontAwesomeIconType.SOLID_SORT_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_SORT_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_SORT_NUMERIC_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_SORT_NUMERIC_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_SORT_NUMERIC_UP:
                    unicode = FontAwesomeUnicode.SOLID_SORT_NUMERIC_UP;
                    break;

                case FontAwesomeIconType.SOLID_SORT_UP:
                    unicode = FontAwesomeUnicode.SOLID_SORT_UP;
                    break;

                case FontAwesomeIconType.SOLID_SPA:
                    unicode = FontAwesomeUnicode.SOLID_SPA;
                    break;

                case FontAwesomeIconType.SOLID_SPACE_SHUTTLE:
                    unicode = FontAwesomeUnicode.SOLID_SPACE_SHUTTLE;
                    break;

                case FontAwesomeIconType.SOLID_SPIDER:
                    unicode = FontAwesomeUnicode.SOLID_SPIDER;
                    break;

                case FontAwesomeIconType.SOLID_SPINNER:
                    unicode = FontAwesomeUnicode.SOLID_SPINNER;
                    break;

                case FontAwesomeIconType.SOLID_SPLOTCH:
                    unicode = FontAwesomeUnicode.SOLID_SPLOTCH;
                    break;

                case FontAwesomeIconType.SOLID_SPRAY_CAN:
                    unicode = FontAwesomeUnicode.SOLID_SPRAY_CAN;
                    break;

                case FontAwesomeIconType.SOLID_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_SQUARE_FULL:
                    unicode = FontAwesomeUnicode.SOLID_SQUARE_FULL;
                    break;

                case FontAwesomeIconType.SOLID_SQUARE_ROOT_ALT:
                    unicode = FontAwesomeUnicode.SOLID_SQUARE_ROOT_ALT;
                    break;

                case FontAwesomeIconType.SOLID_STAMP:
                    unicode = FontAwesomeUnicode.SOLID_STAMP;
                    break;

                case FontAwesomeIconType.SOLID_STAR:
                    unicode = FontAwesomeUnicode.SOLID_STAR;
                    break;

                case FontAwesomeIconType.SOLID_STAR_AND_CRESCENT:
                    unicode = FontAwesomeUnicode.SOLID_STAR_AND_CRESCENT;
                    break;

                case FontAwesomeIconType.SOLID_STAR_HALF:
                    unicode = FontAwesomeUnicode.SOLID_STAR_HALF;
                    break;

                case FontAwesomeIconType.SOLID_STAR_HALF_ALT:
                    unicode = FontAwesomeUnicode.SOLID_STAR_HALF_ALT;
                    break;

                case FontAwesomeIconType.SOLID_STAR_OF_DAVID:
                    unicode = FontAwesomeUnicode.SOLID_STAR_OF_DAVID;
                    break;

                case FontAwesomeIconType.SOLID_STAR_OF_LIFE:
                    unicode = FontAwesomeUnicode.SOLID_STAR_OF_LIFE;
                    break;

                case FontAwesomeIconType.SOLID_STEP_BACKWARD:
                    unicode = FontAwesomeUnicode.SOLID_STEP_BACKWARD;
                    break;

                case FontAwesomeIconType.SOLID_STEP_FORWARD:
                    unicode = FontAwesomeUnicode.SOLID_STEP_FORWARD;
                    break;

                case FontAwesomeIconType.SOLID_STETHOSCOPE:
                    unicode = FontAwesomeUnicode.SOLID_STETHOSCOPE;
                    break;

                case FontAwesomeIconType.SOLID_STICKY_NOTE:
                    unicode = FontAwesomeUnicode.SOLID_STICKY_NOTE;
                    break;

                case FontAwesomeIconType.SOLID_STOP:
                    unicode = FontAwesomeUnicode.SOLID_STOP;
                    break;

                case FontAwesomeIconType.SOLID_STOP_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_STOP_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_STOPWATCH:
                    unicode = FontAwesomeUnicode.SOLID_STOPWATCH;
                    break;

                case FontAwesomeIconType.SOLID_STORE:
                    unicode = FontAwesomeUnicode.SOLID_STORE;
                    break;

                case FontAwesomeIconType.SOLID_STORE_ALT:
                    unicode = FontAwesomeUnicode.SOLID_STORE_ALT;
                    break;

                case FontAwesomeIconType.SOLID_STREAM:
                    unicode = FontAwesomeUnicode.SOLID_STREAM;
                    break;

                case FontAwesomeIconType.SOLID_STREET_VIEW:
                    unicode = FontAwesomeUnicode.SOLID_STREET_VIEW;
                    break;

                case FontAwesomeIconType.SOLID_STRIKETHROUGH:
                    unicode = FontAwesomeUnicode.SOLID_STRIKETHROUGH;
                    break;

                case FontAwesomeIconType.SOLID_STROOPWAFEL:
                    unicode = FontAwesomeUnicode.SOLID_STROOPWAFEL;
                    break;

                case FontAwesomeIconType.SOLID_SUBSCRIPT:
                    unicode = FontAwesomeUnicode.SOLID_SUBSCRIPT;
                    break;

                case FontAwesomeIconType.SOLID_SUBWAY:
                    unicode = FontAwesomeUnicode.SOLID_SUBWAY;
                    break;

                case FontAwesomeIconType.SOLID_SUITCASE:
                    unicode = FontAwesomeUnicode.SOLID_SUITCASE;
                    break;

                case FontAwesomeIconType.SOLID_SUITCASE_ROLLING:
                    unicode = FontAwesomeUnicode.SOLID_SUITCASE_ROLLING;
                    break;

                case FontAwesomeIconType.SOLID_SUN:
                    unicode = FontAwesomeUnicode.SOLID_SUN;
                    break;

                case FontAwesomeIconType.SOLID_SUPERSCRIPT:
                    unicode = FontAwesomeUnicode.SOLID_SUPERSCRIPT;
                    break;

                case FontAwesomeIconType.SOLID_SURPRISE:
                    unicode = FontAwesomeUnicode.SOLID_SURPRISE;
                    break;

                case FontAwesomeIconType.SOLID_SWATCHBOOK:
                    unicode = FontAwesomeUnicode.SOLID_SWATCHBOOK;
                    break;

                case FontAwesomeIconType.SOLID_SWIMMER:
                    unicode = FontAwesomeUnicode.SOLID_SWIMMER;
                    break;

                case FontAwesomeIconType.SOLID_SWIMMING_POOL:
                    unicode = FontAwesomeUnicode.SOLID_SWIMMING_POOL;
                    break;

                case FontAwesomeIconType.SOLID_SYNAGOGUE:
                    unicode = FontAwesomeUnicode.SOLID_SYNAGOGUE;
                    break;

                case FontAwesomeIconType.SOLID_SYNC:
                    unicode = FontAwesomeUnicode.SOLID_SYNC;
                    break;

                case FontAwesomeIconType.SOLID_SYNC_ALT:
                    unicode = FontAwesomeUnicode.SOLID_SYNC_ALT;
                    break;

                case FontAwesomeIconType.SOLID_SYRINGE:
                    unicode = FontAwesomeUnicode.SOLID_SYRINGE;
                    break;

                case FontAwesomeIconType.SOLID_TABLE:
                    unicode = FontAwesomeUnicode.SOLID_TABLE;
                    break;

                case FontAwesomeIconType.SOLID_TABLE_TENNIS:
                    unicode = FontAwesomeUnicode.SOLID_TABLE_TENNIS;
                    break;

                case FontAwesomeIconType.SOLID_TABLET:
                    unicode = FontAwesomeUnicode.SOLID_TABLET;
                    break;

                case FontAwesomeIconType.SOLID_TABLET_ALT:
                    unicode = FontAwesomeUnicode.SOLID_TABLET_ALT;
                    break;

                case FontAwesomeIconType.SOLID_TABLETS:
                    unicode = FontAwesomeUnicode.SOLID_TABLETS;
                    break;

                case FontAwesomeIconType.SOLID_TACHOMETER_ALT:
                    unicode = FontAwesomeUnicode.SOLID_TACHOMETER_ALT;
                    break;

                case FontAwesomeIconType.SOLID_TAG:
                    unicode = FontAwesomeUnicode.SOLID_TAG;
                    break;

                case FontAwesomeIconType.SOLID_TAGS:
                    unicode = FontAwesomeUnicode.SOLID_TAGS;
                    break;

                case FontAwesomeIconType.SOLID_TAPE:
                    unicode = FontAwesomeUnicode.SOLID_TAPE;
                    break;

                case FontAwesomeIconType.SOLID_TASKS:
                    unicode = FontAwesomeUnicode.SOLID_TASKS;
                    break;

                case FontAwesomeIconType.SOLID_TAXI:
                    unicode = FontAwesomeUnicode.SOLID_TAXI;
                    break;

                case FontAwesomeIconType.SOLID_TEETH:
                    unicode = FontAwesomeUnicode.SOLID_TEETH;
                    break;

                case FontAwesomeIconType.SOLID_TEETH_OPEN:
                    unicode = FontAwesomeUnicode.SOLID_TEETH_OPEN;
                    break;

                case FontAwesomeIconType.SOLID_TEMPERATURE_HIGH:
                    unicode = FontAwesomeUnicode.SOLID_TEMPERATURE_HIGH;
                    break;

                case FontAwesomeIconType.SOLID_TEMPERATURE_LOW:
                    unicode = FontAwesomeUnicode.SOLID_TEMPERATURE_LOW;
                    break;

                case FontAwesomeIconType.SOLID_TERMINAL:
                    unicode = FontAwesomeUnicode.SOLID_TERMINAL;
                    break;

                case FontAwesomeIconType.SOLID_TEXT_HEIGHT:
                    unicode = FontAwesomeUnicode.SOLID_TEXT_HEIGHT;
                    break;

                case FontAwesomeIconType.SOLID_TEXT_WIDTH:
                    unicode = FontAwesomeUnicode.SOLID_TEXT_WIDTH;
                    break;

                case FontAwesomeIconType.SOLID_TH:
                    unicode = FontAwesomeUnicode.SOLID_TH;
                    break;

                case FontAwesomeIconType.SOLID_TH_LARGE:
                    unicode = FontAwesomeUnicode.SOLID_TH_LARGE;
                    break;

                case FontAwesomeIconType.SOLID_TH_LIST:
                    unicode = FontAwesomeUnicode.SOLID_TH_LIST;
                    break;

                case FontAwesomeIconType.SOLID_THEATER_MASKS:
                    unicode = FontAwesomeUnicode.SOLID_THEATER_MASKS;
                    break;

                case FontAwesomeIconType.SOLID_THERMOMETER:
                    unicode = FontAwesomeUnicode.SOLID_THERMOMETER;
                    break;

                case FontAwesomeIconType.SOLID_THERMOMETER_EMPTY:
                    unicode = FontAwesomeUnicode.SOLID_THERMOMETER_EMPTY;
                    break;

                case FontAwesomeIconType.SOLID_THERMOMETER_FULL:
                    unicode = FontAwesomeUnicode.SOLID_THERMOMETER_FULL;
                    break;

                case FontAwesomeIconType.SOLID_THERMOMETER_HALF:
                    unicode = FontAwesomeUnicode.SOLID_THERMOMETER_HALF;
                    break;

                case FontAwesomeIconType.SOLID_THERMOMETER_QUARTER:
                    unicode = FontAwesomeUnicode.SOLID_THERMOMETER_QUARTER;
                    break;

                case FontAwesomeIconType.SOLID_THERMOMETER_THREE_QUARTERS:
                    unicode = FontAwesomeUnicode.SOLID_THERMOMETER_THREE_QUARTERS;
                    break;

                case FontAwesomeIconType.SOLID_THUMBS_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_THUMBS_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_THUMBS_UP:
                    unicode = FontAwesomeUnicode.SOLID_THUMBS_UP;
                    break;

                case FontAwesomeIconType.SOLID_THUMBTACK:
                    unicode = FontAwesomeUnicode.SOLID_THUMBTACK;
                    break;

                case FontAwesomeIconType.SOLID_TICKET_ALT:
                    unicode = FontAwesomeUnicode.SOLID_TICKET_ALT;
                    break;

                case FontAwesomeIconType.SOLID_TIMES:
                    unicode = FontAwesomeUnicode.SOLID_TIMES;
                    break;

                case FontAwesomeIconType.SOLID_TIMES_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_TIMES_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_TINT:
                    unicode = FontAwesomeUnicode.SOLID_TINT;
                    break;

                case FontAwesomeIconType.SOLID_TINT_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_TINT_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_TIRED:
                    unicode = FontAwesomeUnicode.SOLID_TIRED;
                    break;

                case FontAwesomeIconType.SOLID_TOGGLE_OFF:
                    unicode = FontAwesomeUnicode.SOLID_TOGGLE_OFF;
                    break;

                case FontAwesomeIconType.SOLID_TOGGLE_ON:
                    unicode = FontAwesomeUnicode.SOLID_TOGGLE_ON;
                    break;

                case FontAwesomeIconType.SOLID_TOILET_PAPER:
                    unicode = FontAwesomeUnicode.SOLID_TOILET_PAPER;
                    break;

                case FontAwesomeIconType.SOLID_TOOLBOX:
                    unicode = FontAwesomeUnicode.SOLID_TOOLBOX;
                    break;

                case FontAwesomeIconType.SOLID_TOOTH:
                    unicode = FontAwesomeUnicode.SOLID_TOOTH;
                    break;

                case FontAwesomeIconType.SOLID_TORAH:
                    unicode = FontAwesomeUnicode.SOLID_TORAH;
                    break;

                case FontAwesomeIconType.SOLID_TORII_GATE:
                    unicode = FontAwesomeUnicode.SOLID_TORII_GATE;
                    break;

                case FontAwesomeIconType.SOLID_TRACTOR:
                    unicode = FontAwesomeUnicode.SOLID_TRACTOR;
                    break;

                case FontAwesomeIconType.SOLID_TRADEMARK:
                    unicode = FontAwesomeUnicode.SOLID_TRADEMARK;
                    break;

                case FontAwesomeIconType.SOLID_TRAFFIC_LIGHT:
                    unicode = FontAwesomeUnicode.SOLID_TRAFFIC_LIGHT;
                    break;

                case FontAwesomeIconType.SOLID_TRAIN:
                    unicode = FontAwesomeUnicode.SOLID_TRAIN;
                    break;

                case FontAwesomeIconType.SOLID_TRANSGENDER:
                    unicode = FontAwesomeUnicode.SOLID_TRANSGENDER;
                    break;

                case FontAwesomeIconType.SOLID_TRANSGENDER_ALT:
                    unicode = FontAwesomeUnicode.SOLID_TRANSGENDER_ALT;
                    break;

                case FontAwesomeIconType.SOLID_TRASH:
                    unicode = FontAwesomeUnicode.SOLID_TRASH;
                    break;

                case FontAwesomeIconType.SOLID_TRASH_ALT:
                    unicode = FontAwesomeUnicode.SOLID_TRASH_ALT;
                    break;

                case FontAwesomeIconType.SOLID_TREE:
                    unicode = FontAwesomeUnicode.SOLID_TREE;
                    break;

                case FontAwesomeIconType.SOLID_TROPHY:
                    unicode = FontAwesomeUnicode.SOLID_TROPHY;
                    break;

                case FontAwesomeIconType.SOLID_TRUCK:
                    unicode = FontAwesomeUnicode.SOLID_TRUCK;
                    break;

                case FontAwesomeIconType.SOLID_TRUCK_LOADING:
                    unicode = FontAwesomeUnicode.SOLID_TRUCK_LOADING;
                    break;

                case FontAwesomeIconType.SOLID_TRUCK_MONSTER:
                    unicode = FontAwesomeUnicode.SOLID_TRUCK_MONSTER;
                    break;

                case FontAwesomeIconType.SOLID_TRUCK_MOVING:
                    unicode = FontAwesomeUnicode.SOLID_TRUCK_MOVING;
                    break;

                case FontAwesomeIconType.SOLID_TRUCK_PICKUP:
                    unicode = FontAwesomeUnicode.SOLID_TRUCK_PICKUP;
                    break;

                case FontAwesomeIconType.SOLID_TSHIRT:
                    unicode = FontAwesomeUnicode.SOLID_TSHIRT;
                    break;

                case FontAwesomeIconType.SOLID_TTY:
                    unicode = FontAwesomeUnicode.SOLID_TTY;
                    break;

                case FontAwesomeIconType.SOLID_TV:
                    unicode = FontAwesomeUnicode.SOLID_TV;
                    break;

                case FontAwesomeIconType.SOLID_UMBRELLA:
                    unicode = FontAwesomeUnicode.SOLID_UMBRELLA;
                    break;

                case FontAwesomeIconType.SOLID_UMBRELLA_BEACH:
                    unicode = FontAwesomeUnicode.SOLID_UMBRELLA_BEACH;
                    break;

                case FontAwesomeIconType.SOLID_UNDERLINE:
                    unicode = FontAwesomeUnicode.SOLID_UNDERLINE;
                    break;

                case FontAwesomeIconType.SOLID_UNDO:
                    unicode = FontAwesomeUnicode.SOLID_UNDO;
                    break;

                case FontAwesomeIconType.SOLID_UNDO_ALT:
                    unicode = FontAwesomeUnicode.SOLID_UNDO_ALT;
                    break;

                case FontAwesomeIconType.SOLID_UNIVERSAL_ACCESS:
                    unicode = FontAwesomeUnicode.SOLID_UNIVERSAL_ACCESS;
                    break;

                case FontAwesomeIconType.SOLID_UNIVERSITY:
                    unicode = FontAwesomeUnicode.SOLID_UNIVERSITY;
                    break;

                case FontAwesomeIconType.SOLID_UNLINK:
                    unicode = FontAwesomeUnicode.SOLID_UNLINK;
                    break;

                case FontAwesomeIconType.SOLID_UNLOCK:
                    unicode = FontAwesomeUnicode.SOLID_UNLOCK;
                    break;

                case FontAwesomeIconType.SOLID_UNLOCK_ALT:
                    unicode = FontAwesomeUnicode.SOLID_UNLOCK_ALT;
                    break;

                case FontAwesomeIconType.SOLID_UPLOAD:
                    unicode = FontAwesomeUnicode.SOLID_UPLOAD;
                    break;

                case FontAwesomeIconType.SOLID_USER:
                    unicode = FontAwesomeUnicode.SOLID_USER;
                    break;

                case FontAwesomeIconType.SOLID_USER_ALT:
                    unicode = FontAwesomeUnicode.SOLID_USER_ALT;
                    break;

                case FontAwesomeIconType.SOLID_USER_ALT_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_USER_ALT_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_USER_ASTRONAUT:
                    unicode = FontAwesomeUnicode.SOLID_USER_ASTRONAUT;
                    break;

                case FontAwesomeIconType.SOLID_USER_CHECK:
                    unicode = FontAwesomeUnicode.SOLID_USER_CHECK;
                    break;

                case FontAwesomeIconType.SOLID_USER_CIRCLE:
                    unicode = FontAwesomeUnicode.SOLID_USER_CIRCLE;
                    break;

                case FontAwesomeIconType.SOLID_USER_CLOCK:
                    unicode = FontAwesomeUnicode.SOLID_USER_CLOCK;
                    break;

                case FontAwesomeIconType.SOLID_USER_COG:
                    unicode = FontAwesomeUnicode.SOLID_USER_COG;
                    break;

                case FontAwesomeIconType.SOLID_USER_EDIT:
                    unicode = FontAwesomeUnicode.SOLID_USER_EDIT;
                    break;

                case FontAwesomeIconType.SOLID_USER_FRIENDS:
                    unicode = FontAwesomeUnicode.SOLID_USER_FRIENDS;
                    break;

                case FontAwesomeIconType.SOLID_USER_GRADUATE:
                    unicode = FontAwesomeUnicode.SOLID_USER_GRADUATE;
                    break;

                case FontAwesomeIconType.SOLID_USER_INJURED:
                    unicode = FontAwesomeUnicode.SOLID_USER_INJURED;
                    break;

                case FontAwesomeIconType.SOLID_USER_LOCK:
                    unicode = FontAwesomeUnicode.SOLID_USER_LOCK;
                    break;

                case FontAwesomeIconType.SOLID_USER_MD:
                    unicode = FontAwesomeUnicode.SOLID_USER_MD;
                    break;

                case FontAwesomeIconType.SOLID_USER_MINUS:
                    unicode = FontAwesomeUnicode.SOLID_USER_MINUS;
                    break;

                case FontAwesomeIconType.SOLID_USER_NINJA:
                    unicode = FontAwesomeUnicode.SOLID_USER_NINJA;
                    break;

                case FontAwesomeIconType.SOLID_USER_PLUS:
                    unicode = FontAwesomeUnicode.SOLID_USER_PLUS;
                    break;

                case FontAwesomeIconType.SOLID_USER_SECRET:
                    unicode = FontAwesomeUnicode.SOLID_USER_SECRET;
                    break;

                case FontAwesomeIconType.SOLID_USER_SHIELD:
                    unicode = FontAwesomeUnicode.SOLID_USER_SHIELD;
                    break;

                case FontAwesomeIconType.SOLID_USER_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_USER_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_USER_TAG:
                    unicode = FontAwesomeUnicode.SOLID_USER_TAG;
                    break;

                case FontAwesomeIconType.SOLID_USER_TIE:
                    unicode = FontAwesomeUnicode.SOLID_USER_TIE;
                    break;

                case FontAwesomeIconType.SOLID_USER_TIMES:
                    unicode = FontAwesomeUnicode.SOLID_USER_TIMES;
                    break;

                case FontAwesomeIconType.SOLID_USERS:
                    unicode = FontAwesomeUnicode.SOLID_USERS;
                    break;

                case FontAwesomeIconType.SOLID_USERS_COG:
                    unicode = FontAwesomeUnicode.SOLID_USERS_COG;
                    break;

                case FontAwesomeIconType.SOLID_UTENSIL_SPOON:
                    unicode = FontAwesomeUnicode.SOLID_UTENSIL_SPOON;
                    break;

                case FontAwesomeIconType.SOLID_UTENSILS:
                    unicode = FontAwesomeUnicode.SOLID_UTENSILS;
                    break;

                case FontAwesomeIconType.SOLID_VECTOR_SQUARE:
                    unicode = FontAwesomeUnicode.SOLID_VECTOR_SQUARE;
                    break;

                case FontAwesomeIconType.SOLID_VENUS:
                    unicode = FontAwesomeUnicode.SOLID_VENUS;
                    break;

                case FontAwesomeIconType.SOLID_VENUS_DOUBLE:
                    unicode = FontAwesomeUnicode.SOLID_VENUS_DOUBLE;
                    break;

                case FontAwesomeIconType.SOLID_VENUS_MARS:
                    unicode = FontAwesomeUnicode.SOLID_VENUS_MARS;
                    break;

                case FontAwesomeIconType.SOLID_VIAL:
                    unicode = FontAwesomeUnicode.SOLID_VIAL;
                    break;

                case FontAwesomeIconType.SOLID_VIALS:
                    unicode = FontAwesomeUnicode.SOLID_VIALS;
                    break;

                case FontAwesomeIconType.SOLID_VIDEO:
                    unicode = FontAwesomeUnicode.SOLID_VIDEO;
                    break;

                case FontAwesomeIconType.SOLID_VIDEO_SLASH:
                    unicode = FontAwesomeUnicode.SOLID_VIDEO_SLASH;
                    break;

                case FontAwesomeIconType.SOLID_VIHARA:
                    unicode = FontAwesomeUnicode.SOLID_VIHARA;
                    break;

                case FontAwesomeIconType.SOLID_VOLLEYBALL_BALL:
                    unicode = FontAwesomeUnicode.SOLID_VOLLEYBALL_BALL;
                    break;

                case FontAwesomeIconType.SOLID_VOLUME_DOWN:
                    unicode = FontAwesomeUnicode.SOLID_VOLUME_DOWN;
                    break;

                case FontAwesomeIconType.SOLID_VOLUME_MUTE:
                    unicode = FontAwesomeUnicode.SOLID_VOLUME_MUTE;
                    break;

                case FontAwesomeIconType.SOLID_VOLUME_OFF:
                    unicode = FontAwesomeUnicode.SOLID_VOLUME_OFF;
                    break;

                case FontAwesomeIconType.SOLID_VOLUME_UP:
                    unicode = FontAwesomeUnicode.SOLID_VOLUME_UP;
                    break;

                case FontAwesomeIconType.SOLID_VOTE_YEA:
                    unicode = FontAwesomeUnicode.SOLID_VOTE_YEA;
                    break;

                case FontAwesomeIconType.SOLID_VR_CARDBOARD:
                    unicode = FontAwesomeUnicode.SOLID_VR_CARDBOARD;
                    break;

                case FontAwesomeIconType.SOLID_WALKING:
                    unicode = FontAwesomeUnicode.SOLID_WALKING;
                    break;

                case FontAwesomeIconType.SOLID_WALLET:
                    unicode = FontAwesomeUnicode.SOLID_WALLET;
                    break;

                case FontAwesomeIconType.SOLID_WAREHOUSE:
                    unicode = FontAwesomeUnicode.SOLID_WAREHOUSE;
                    break;

                case FontAwesomeIconType.SOLID_WATER:
                    unicode = FontAwesomeUnicode.SOLID_WATER;
                    break;

                case FontAwesomeIconType.SOLID_WEIGHT:
                    unicode = FontAwesomeUnicode.SOLID_WEIGHT;
                    break;

                case FontAwesomeIconType.SOLID_WEIGHT_HANGING:
                    unicode = FontAwesomeUnicode.SOLID_WEIGHT_HANGING;
                    break;

                case FontAwesomeIconType.SOLID_WHEELCHAIR:
                    unicode = FontAwesomeUnicode.SOLID_WHEELCHAIR;
                    break;

                case FontAwesomeIconType.SOLID_WIFI:
                    unicode = FontAwesomeUnicode.SOLID_WIFI;
                    break;

                case FontAwesomeIconType.SOLID_WIND:
                    unicode = FontAwesomeUnicode.SOLID_WIND;
                    break;

                case FontAwesomeIconType.SOLID_WINDOW_CLOSE:
                    unicode = FontAwesomeUnicode.SOLID_WINDOW_CLOSE;
                    break;

                case FontAwesomeIconType.SOLID_WINDOW_MAXIMIZE:
                    unicode = FontAwesomeUnicode.SOLID_WINDOW_MAXIMIZE;
                    break;

                case FontAwesomeIconType.SOLID_WINDOW_MINIMIZE:
                    unicode = FontAwesomeUnicode.SOLID_WINDOW_MINIMIZE;
                    break;

                case FontAwesomeIconType.SOLID_WINDOW_RESTORE:
                    unicode = FontAwesomeUnicode.SOLID_WINDOW_RESTORE;
                    break;

                case FontAwesomeIconType.SOLID_WINE_BOTTLE:
                    unicode = FontAwesomeUnicode.SOLID_WINE_BOTTLE;
                    break;

                case FontAwesomeIconType.SOLID_WINE_GLASS:
                    unicode = FontAwesomeUnicode.SOLID_WINE_GLASS;
                    break;

                case FontAwesomeIconType.SOLID_WINE_GLASS_ALT:
                    unicode = FontAwesomeUnicode.SOLID_WINE_GLASS_ALT;
                    break;

                case FontAwesomeIconType.SOLID_WON_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_WON_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_WRENCH:
                    unicode = FontAwesomeUnicode.SOLID_WRENCH;
                    break;

                case FontAwesomeIconType.SOLID_X_RAY:
                    unicode = FontAwesomeUnicode.SOLID_X_RAY;
                    break;

                case FontAwesomeIconType.SOLID_YEN_SIGN:
                    unicode = FontAwesomeUnicode.SOLID_YEN_SIGN;
                    break;

                case FontAwesomeIconType.SOLID_YIN_YANG:
                    unicode = FontAwesomeUnicode.SOLID_YIN_YANG;
                    break;

                case FontAwesomeIconType.REGULAR_ADDRESS_BOOK:
                    unicode = FontAwesomeUnicode.REGULAR_ADDRESS_BOOK;
                    break;

                case FontAwesomeIconType.REGULAR_ADDRESS_CARD:
                    unicode = FontAwesomeUnicode.REGULAR_ADDRESS_CARD;
                    break;

                case FontAwesomeIconType.REGULAR_ANGRY:
                    unicode = FontAwesomeUnicode.REGULAR_ANGRY;
                    break;

                case FontAwesomeIconType.REGULAR_ARROW_ALT_CIRCLE_DOWN:
                    unicode = FontAwesomeUnicode.REGULAR_ARROW_ALT_CIRCLE_DOWN;
                    break;

                case FontAwesomeIconType.REGULAR_ARROW_ALT_CIRCLE_LEFT:
                    unicode = FontAwesomeUnicode.REGULAR_ARROW_ALT_CIRCLE_LEFT;
                    break;

                case FontAwesomeIconType.REGULAR_ARROW_ALT_CIRCLE_RIGHT:
                    unicode = FontAwesomeUnicode.REGULAR_ARROW_ALT_CIRCLE_RIGHT;
                    break;

                case FontAwesomeIconType.REGULAR_ARROW_ALT_CIRCLE_UP:
                    unicode = FontAwesomeUnicode.REGULAR_ARROW_ALT_CIRCLE_UP;
                    break;

                case FontAwesomeIconType.REGULAR_BELL:
                    unicode = FontAwesomeUnicode.REGULAR_BELL;
                    break;

                case FontAwesomeIconType.REGULAR_BELL_SLASH:
                    unicode = FontAwesomeUnicode.REGULAR_BELL_SLASH;
                    break;

                case FontAwesomeIconType.REGULAR_BOOKMARK:
                    unicode = FontAwesomeUnicode.REGULAR_BOOKMARK;
                    break;

                case FontAwesomeIconType.REGULAR_BUILDING:
                    unicode = FontAwesomeUnicode.REGULAR_BUILDING;
                    break;

                case FontAwesomeIconType.REGULAR_CALENDAR:
                    unicode = FontAwesomeUnicode.REGULAR_CALENDAR;
                    break;

                case FontAwesomeIconType.REGULAR_CALENDAR_ALT:
                    unicode = FontAwesomeUnicode.REGULAR_CALENDAR_ALT;
                    break;

                case FontAwesomeIconType.REGULAR_CALENDAR_CHECK:
                    unicode = FontAwesomeUnicode.REGULAR_CALENDAR_CHECK;
                    break;

                case FontAwesomeIconType.REGULAR_CALENDAR_MINUS:
                    unicode = FontAwesomeUnicode.REGULAR_CALENDAR_MINUS;
                    break;

                case FontAwesomeIconType.REGULAR_CALENDAR_PLUS:
                    unicode = FontAwesomeUnicode.REGULAR_CALENDAR_PLUS;
                    break;

                case FontAwesomeIconType.REGULAR_CALENDAR_TIMES:
                    unicode = FontAwesomeUnicode.REGULAR_CALENDAR_TIMES;
                    break;

                case FontAwesomeIconType.REGULAR_CARET_SQUARE_DOWN:
                    unicode = FontAwesomeUnicode.REGULAR_CARET_SQUARE_DOWN;
                    break;

                case FontAwesomeIconType.REGULAR_CARET_SQUARE_LEFT:
                    unicode = FontAwesomeUnicode.REGULAR_CARET_SQUARE_LEFT;
                    break;

                case FontAwesomeIconType.REGULAR_CARET_SQUARE_RIGHT:
                    unicode = FontAwesomeUnicode.REGULAR_CARET_SQUARE_RIGHT;
                    break;

                case FontAwesomeIconType.REGULAR_CARET_SQUARE_UP:
                    unicode = FontAwesomeUnicode.REGULAR_CARET_SQUARE_UP;
                    break;

                case FontAwesomeIconType.REGULAR_CHART_BAR:
                    unicode = FontAwesomeUnicode.REGULAR_CHART_BAR;
                    break;

                case FontAwesomeIconType.REGULAR_CHECK_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_CHECK_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_CHECK_SQUARE:
                    unicode = FontAwesomeUnicode.REGULAR_CHECK_SQUARE;
                    break;

                case FontAwesomeIconType.REGULAR_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_CLIPBOARD:
                    unicode = FontAwesomeUnicode.REGULAR_CLIPBOARD;
                    break;

                case FontAwesomeIconType.REGULAR_CLOCK:
                    unicode = FontAwesomeUnicode.REGULAR_CLOCK;
                    break;

                case FontAwesomeIconType.REGULAR_CLONE:
                    unicode = FontAwesomeUnicode.REGULAR_CLONE;
                    break;

                case FontAwesomeIconType.REGULAR_CLOSED_CAPTIONING:
                    unicode = FontAwesomeUnicode.REGULAR_CLOSED_CAPTIONING;
                    break;

                case FontAwesomeIconType.REGULAR_COMMENT:
                    unicode = FontAwesomeUnicode.REGULAR_COMMENT;
                    break;

                case FontAwesomeIconType.REGULAR_COMMENT_ALT:
                    unicode = FontAwesomeUnicode.REGULAR_COMMENT_ALT;
                    break;

                case FontAwesomeIconType.REGULAR_COMMENT_DOTS:
                    unicode = FontAwesomeUnicode.REGULAR_COMMENT_DOTS;
                    break;

                case FontAwesomeIconType.REGULAR_COMMENTS:
                    unicode = FontAwesomeUnicode.REGULAR_COMMENTS;
                    break;

                case FontAwesomeIconType.REGULAR_COMPASS:
                    unicode = FontAwesomeUnicode.REGULAR_COMPASS;
                    break;

                case FontAwesomeIconType.REGULAR_COPY:
                    unicode = FontAwesomeUnicode.REGULAR_COPY;
                    break;

                case FontAwesomeIconType.REGULAR_COPYRIGHT:
                    unicode = FontAwesomeUnicode.REGULAR_COPYRIGHT;
                    break;

                case FontAwesomeIconType.REGULAR_CREDIT_CARD:
                    unicode = FontAwesomeUnicode.REGULAR_CREDIT_CARD;
                    break;

                case FontAwesomeIconType.REGULAR_DIZZY:
                    unicode = FontAwesomeUnicode.REGULAR_DIZZY;
                    break;

                case FontAwesomeIconType.REGULAR_DOT_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_DOT_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_EDIT:
                    unicode = FontAwesomeUnicode.REGULAR_EDIT;
                    break;

                case FontAwesomeIconType.REGULAR_ENVELOPE:
                    unicode = FontAwesomeUnicode.REGULAR_ENVELOPE;
                    break;

                case FontAwesomeIconType.REGULAR_ENVELOPE_OPEN:
                    unicode = FontAwesomeUnicode.REGULAR_ENVELOPE_OPEN;
                    break;

                case FontAwesomeIconType.REGULAR_EYE:
                    unicode = FontAwesomeUnicode.REGULAR_EYE;
                    break;

                case FontAwesomeIconType.REGULAR_EYE_SLASH:
                    unicode = FontAwesomeUnicode.REGULAR_EYE_SLASH;
                    break;

                case FontAwesomeIconType.REGULAR_FILE:
                    unicode = FontAwesomeUnicode.REGULAR_FILE;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_ALT:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_ALT;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_ARCHIVE:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_ARCHIVE;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_AUDIO:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_AUDIO;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_CODE:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_CODE;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_EXCEL:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_EXCEL;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_IMAGE:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_IMAGE;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_PDF:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_PDF;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_POWERPOINT:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_POWERPOINT;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_VIDEO:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_VIDEO;
                    break;

                case FontAwesomeIconType.REGULAR_FILE_WORD:
                    unicode = FontAwesomeUnicode.REGULAR_FILE_WORD;
                    break;

                case FontAwesomeIconType.REGULAR_FLAG:
                    unicode = FontAwesomeUnicode.REGULAR_FLAG;
                    break;

                case FontAwesomeIconType.REGULAR_FLUSHED:
                    unicode = FontAwesomeUnicode.REGULAR_FLUSHED;
                    break;

                case FontAwesomeIconType.REGULAR_FOLDER:
                    unicode = FontAwesomeUnicode.REGULAR_FOLDER;
                    break;

                case FontAwesomeIconType.REGULAR_FOLDER_OPEN:
                    unicode = FontAwesomeUnicode.REGULAR_FOLDER_OPEN;
                    break;

                case FontAwesomeIconType.REGULAR_FROWN:
                    unicode = FontAwesomeUnicode.REGULAR_FROWN;
                    break;

                case FontAwesomeIconType.REGULAR_FROWN_OPEN:
                    unicode = FontAwesomeUnicode.REGULAR_FROWN_OPEN;
                    break;

                case FontAwesomeIconType.REGULAR_FUTBOL:
                    unicode = FontAwesomeUnicode.REGULAR_FUTBOL;
                    break;

                case FontAwesomeIconType.REGULAR_GEM:
                    unicode = FontAwesomeUnicode.REGULAR_GEM;
                    break;

                case FontAwesomeIconType.REGULAR_GRIMACE:
                    unicode = FontAwesomeUnicode.REGULAR_GRIMACE;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_ALT:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_ALT;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_BEAM:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_BEAM;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_BEAM_SWEAT:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_BEAM_SWEAT;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_HEARTS:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_HEARTS;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_SQUINT:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_SQUINT;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_SQUINT_TEARS:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_SQUINT_TEARS;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_STARS:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_STARS;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_TEARS:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_TEARS;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_TONGUE:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_TONGUE;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_TONGUE_SQUINT:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_TONGUE_SQUINT;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_TONGUE_WINK:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_TONGUE_WINK;
                    break;

                case FontAwesomeIconType.REGULAR_GRIN_WINK:
                    unicode = FontAwesomeUnicode.REGULAR_GRIN_WINK;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_LIZARD:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_LIZARD;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_PAPER:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_PAPER;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_PEACE:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_PEACE;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_POINT_DOWN:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_POINT_DOWN;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_POINT_LEFT:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_POINT_LEFT;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_POINT_RIGHT:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_POINT_RIGHT;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_POINT_UP:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_POINT_UP;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_POINTER:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_POINTER;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_ROCK:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_ROCK;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_SCISSORS:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_SCISSORS;
                    break;

                case FontAwesomeIconType.REGULAR_HAND_SPOCK:
                    unicode = FontAwesomeUnicode.REGULAR_HAND_SPOCK;
                    break;

                case FontAwesomeIconType.REGULAR_HANDSHAKE:
                    unicode = FontAwesomeUnicode.REGULAR_HANDSHAKE;
                    break;

                case FontAwesomeIconType.REGULAR_HDD:
                    unicode = FontAwesomeUnicode.REGULAR_HDD;
                    break;

                case FontAwesomeIconType.REGULAR_HEART:
                    unicode = FontAwesomeUnicode.REGULAR_HEART;
                    break;

                case FontAwesomeIconType.REGULAR_HOSPITAL:
                    unicode = FontAwesomeUnicode.REGULAR_HOSPITAL;
                    break;

                case FontAwesomeIconType.REGULAR_HOURGLASS:
                    unicode = FontAwesomeUnicode.REGULAR_HOURGLASS;
                    break;

                case FontAwesomeIconType.REGULAR_ID_BADGE:
                    unicode = FontAwesomeUnicode.REGULAR_ID_BADGE;
                    break;

                case FontAwesomeIconType.REGULAR_ID_CARD:
                    unicode = FontAwesomeUnicode.REGULAR_ID_CARD;
                    break;

                case FontAwesomeIconType.REGULAR_IMAGE:
                    unicode = FontAwesomeUnicode.REGULAR_IMAGE;
                    break;

                case FontAwesomeIconType.REGULAR_IMAGES:
                    unicode = FontAwesomeUnicode.REGULAR_IMAGES;
                    break;

                case FontAwesomeIconType.REGULAR_KEYBOARD:
                    unicode = FontAwesomeUnicode.REGULAR_KEYBOARD;
                    break;

                case FontAwesomeIconType.REGULAR_KISS:
                    unicode = FontAwesomeUnicode.REGULAR_KISS;
                    break;

                case FontAwesomeIconType.REGULAR_KISS_BEAM:
                    unicode = FontAwesomeUnicode.REGULAR_KISS_BEAM;
                    break;

                case FontAwesomeIconType.REGULAR_KISS_WINK_HEART:
                    unicode = FontAwesomeUnicode.REGULAR_KISS_WINK_HEART;
                    break;

                case FontAwesomeIconType.REGULAR_LAUGH:
                    unicode = FontAwesomeUnicode.REGULAR_LAUGH;
                    break;

                case FontAwesomeIconType.REGULAR_LAUGH_BEAM:
                    unicode = FontAwesomeUnicode.REGULAR_LAUGH_BEAM;
                    break;

                case FontAwesomeIconType.REGULAR_LAUGH_SQUINT:
                    unicode = FontAwesomeUnicode.REGULAR_LAUGH_SQUINT;
                    break;

                case FontAwesomeIconType.REGULAR_LAUGH_WINK:
                    unicode = FontAwesomeUnicode.REGULAR_LAUGH_WINK;
                    break;

                case FontAwesomeIconType.REGULAR_LEMON:
                    unicode = FontAwesomeUnicode.REGULAR_LEMON;
                    break;

                case FontAwesomeIconType.REGULAR_LIFE_RING:
                    unicode = FontAwesomeUnicode.REGULAR_LIFE_RING;
                    break;

                case FontAwesomeIconType.REGULAR_LIGHTBULB:
                    unicode = FontAwesomeUnicode.REGULAR_LIGHTBULB;
                    break;

                case FontAwesomeIconType.REGULAR_LIST_ALT:
                    unicode = FontAwesomeUnicode.REGULAR_LIST_ALT;
                    break;

                case FontAwesomeIconType.REGULAR_MAP:
                    unicode = FontAwesomeUnicode.REGULAR_MAP;
                    break;

                case FontAwesomeIconType.REGULAR_MEH:
                    unicode = FontAwesomeUnicode.REGULAR_MEH;
                    break;

                case FontAwesomeIconType.REGULAR_MEH_BLANK:
                    unicode = FontAwesomeUnicode.REGULAR_MEH_BLANK;
                    break;

                case FontAwesomeIconType.REGULAR_MEH_ROLLING_EYES:
                    unicode = FontAwesomeUnicode.REGULAR_MEH_ROLLING_EYES;
                    break;

                case FontAwesomeIconType.REGULAR_MINUS_SQUARE:
                    unicode = FontAwesomeUnicode.REGULAR_MINUS_SQUARE;
                    break;

                case FontAwesomeIconType.REGULAR_MONEY_BILL_ALT:
                    unicode = FontAwesomeUnicode.REGULAR_MONEY_BILL_ALT;
                    break;

                case FontAwesomeIconType.REGULAR_MOON:
                    unicode = FontAwesomeUnicode.REGULAR_MOON;
                    break;

                case FontAwesomeIconType.REGULAR_NEWSPAPER:
                    unicode = FontAwesomeUnicode.REGULAR_NEWSPAPER;
                    break;

                case FontAwesomeIconType.REGULAR_OBJECT_GROUP:
                    unicode = FontAwesomeUnicode.REGULAR_OBJECT_GROUP;
                    break;

                case FontAwesomeIconType.REGULAR_OBJECT_UNGROUP:
                    unicode = FontAwesomeUnicode.REGULAR_OBJECT_UNGROUP;
                    break;

                case FontAwesomeIconType.REGULAR_PAPER_PLANE:
                    unicode = FontAwesomeUnicode.REGULAR_PAPER_PLANE;
                    break;

                case FontAwesomeIconType.REGULAR_PAUSE_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_PAUSE_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_PLAY_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_PLAY_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_PLUS_SQUARE:
                    unicode = FontAwesomeUnicode.REGULAR_PLUS_SQUARE;
                    break;

                case FontAwesomeIconType.REGULAR_QUESTION_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_QUESTION_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_REGISTERED:
                    unicode = FontAwesomeUnicode.REGULAR_REGISTERED;
                    break;

                case FontAwesomeIconType.REGULAR_SAD_CRY:
                    unicode = FontAwesomeUnicode.REGULAR_SAD_CRY;
                    break;

                case FontAwesomeIconType.REGULAR_SAD_TEAR:
                    unicode = FontAwesomeUnicode.REGULAR_SAD_TEAR;
                    break;

                case FontAwesomeIconType.REGULAR_SAVE:
                    unicode = FontAwesomeUnicode.REGULAR_SAVE;
                    break;

                case FontAwesomeIconType.REGULAR_SHARE_SQUARE:
                    unicode = FontAwesomeUnicode.REGULAR_SHARE_SQUARE;
                    break;

                case FontAwesomeIconType.REGULAR_SMILE:
                    unicode = FontAwesomeUnicode.REGULAR_SMILE;
                    break;

                case FontAwesomeIconType.REGULAR_SMILE_BEAM:
                    unicode = FontAwesomeUnicode.REGULAR_SMILE_BEAM;
                    break;

                case FontAwesomeIconType.REGULAR_SMILE_WINK:
                    unicode = FontAwesomeUnicode.REGULAR_SMILE_WINK;
                    break;

                case FontAwesomeIconType.REGULAR_SNOWFLAKE:
                    unicode = FontAwesomeUnicode.REGULAR_SNOWFLAKE;
                    break;

                case FontAwesomeIconType.REGULAR_SQUARE:
                    unicode = FontAwesomeUnicode.REGULAR_SQUARE;
                    break;

                case FontAwesomeIconType.REGULAR_STAR:
                    unicode = FontAwesomeUnicode.REGULAR_STAR;
                    break;

                case FontAwesomeIconType.REGULAR_STAR_HALF:
                    unicode = FontAwesomeUnicode.REGULAR_STAR_HALF;
                    break;

                case FontAwesomeIconType.REGULAR_STICKY_NOTE:
                    unicode = FontAwesomeUnicode.REGULAR_STICKY_NOTE;
                    break;

                case FontAwesomeIconType.REGULAR_STOP_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_STOP_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_SUN:
                    unicode = FontAwesomeUnicode.REGULAR_SUN;
                    break;

                case FontAwesomeIconType.REGULAR_SURPRISE:
                    unicode = FontAwesomeUnicode.REGULAR_SURPRISE;
                    break;

                case FontAwesomeIconType.REGULAR_THUMBS_DOWN:
                    unicode = FontAwesomeUnicode.REGULAR_THUMBS_DOWN;
                    break;

                case FontAwesomeIconType.REGULAR_THUMBS_UP:
                    unicode = FontAwesomeUnicode.REGULAR_THUMBS_UP;
                    break;

                case FontAwesomeIconType.REGULAR_TIMES_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_TIMES_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_TIRED:
                    unicode = FontAwesomeUnicode.REGULAR_TIRED;
                    break;

                case FontAwesomeIconType.REGULAR_TRASH_ALT:
                    unicode = FontAwesomeUnicode.REGULAR_TRASH_ALT;
                    break;

                case FontAwesomeIconType.REGULAR_USER:
                    unicode = FontAwesomeUnicode.REGULAR_USER;
                    break;

                case FontAwesomeIconType.REGULAR_USER_CIRCLE:
                    unicode = FontAwesomeUnicode.REGULAR_USER_CIRCLE;
                    break;

                case FontAwesomeIconType.REGULAR_WINDOW_CLOSE:
                    unicode = FontAwesomeUnicode.REGULAR_WINDOW_CLOSE;
                    break;

                case FontAwesomeIconType.REGULAR_WINDOW_MAXIMIZE:
                    unicode = FontAwesomeUnicode.REGULAR_WINDOW_MAXIMIZE;
                    break;

                case FontAwesomeIconType.REGULAR_WINDOW_MINIMIZE:
                    unicode = FontAwesomeUnicode.REGULAR_WINDOW_MINIMIZE;
                    break;

                case FontAwesomeIconType.REGULAR_WINDOW_RESTORE:
                    unicode = FontAwesomeUnicode.REGULAR_WINDOW_RESTORE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_500PX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_500PX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ACCESSIBLE_ICON:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ACCESSIBLE_ICON;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ACCUSOFT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ACCUSOFT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ACQUISITIONS_INCORPORATED:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ACQUISITIONS_INCORPORATED;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ADN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ADN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ADVERSAL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ADVERSAL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AFFILIATETHEME:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AFFILIATETHEME;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ALGOLIA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ALGOLIA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ALIPAY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ALIPAY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AMAZON:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AMAZON;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AMAZON_PAY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AMAZON_PAY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AMILIA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AMILIA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ANDROID:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ANDROID;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ANGELLIST:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ANGELLIST;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ANGRYCREATIVE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ANGRYCREATIVE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ANGULAR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ANGULAR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_APP_STORE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_APP_STORE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_APP_STORE_IOS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_APP_STORE_IOS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_APPER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_APPER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_APPLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_APPLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_APPLE_PAY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_APPLE_PAY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ASYMMETRIK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ASYMMETRIK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AUDIBLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AUDIBLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AUTOPREFIXER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AUTOPREFIXER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AVIANEX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AVIANEX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AVIATO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AVIATO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_AWS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_AWS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BANDCAMP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BANDCAMP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BEHANCE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BEHANCE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BEHANCE_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BEHANCE_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BIMOBJECT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BIMOBJECT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BITBUCKET:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BITBUCKET;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BITCOIN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BITCOIN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BITY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BITY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BLACK_TIE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BLACK_TIE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BLACKBERRY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BLACKBERRY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BLOGGER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BLOGGER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BLOGGER_B:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BLOGGER_B;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BLUETOOTH:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BLUETOOTH;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BLUETOOTH_B:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BLUETOOTH_B;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BTC:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BTC;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_BUROMOBELEXPERTE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_BUROMOBELEXPERTE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_AMAZON_PAY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_AMAZON_PAY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_AMEX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_AMEX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_APPLE_PAY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_APPLE_PAY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_DINERS_CLUB:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_DINERS_CLUB;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_DISCOVER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_DISCOVER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_JCB:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_JCB;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_MASTERCARD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_MASTERCARD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_PAYPAL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_PAYPAL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_STRIPE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_STRIPE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CC_VISA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CC_VISA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CENTERCODE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CENTERCODE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CHROME:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CHROME;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CLOUDSCALE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CLOUDSCALE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CLOUDSMITH:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CLOUDSMITH;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CLOUDVERSIFY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CLOUDVERSIFY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CODEPEN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CODEPEN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CODIEPIE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CODIEPIE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CONNECTDEVELOP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CONNECTDEVELOP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CONTAO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CONTAO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CPANEL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CPANEL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_BY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_BY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_NC:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_NC;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_NC_EU:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_NC_EU;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_NC_JP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_NC_JP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_ND:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_ND;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_PD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_PD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_PD_ALT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_PD_ALT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_REMIX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_REMIX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_SA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_SA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_SAMPLING:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_SAMPLING;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_SAMPLING_PLUS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_SAMPLING_PLUS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_SHARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_SHARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CREATIVE_COMMONS_ZERO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CREATIVE_COMMONS_ZERO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CRITICAL_ROLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CRITICAL_ROLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CSS3:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CSS3;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CSS3_ALT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CSS3_ALT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_CUTTLEFISH:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_CUTTLEFISH;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_D_AND_D:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_D_AND_D;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_D_AND_D_BEYOND:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_D_AND_D_BEYOND;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DASHCUBE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DASHCUBE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DELICIOUS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DELICIOUS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DEPLOYDOG:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DEPLOYDOG;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DESKPRO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DESKPRO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DEV:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DEV;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DEVIANTART:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DEVIANTART;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DIGG:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DIGG;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DIGITAL_OCEAN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DIGITAL_OCEAN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DISCORD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DISCORD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DISCOURSE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DISCOURSE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DOCHUB:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DOCHUB;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DOCKER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DOCKER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DRAFT2DIGITAL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DRAFT2DIGITAL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DRIBBBLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DRIBBBLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DRIBBBLE_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DRIBBBLE_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DROPBOX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DROPBOX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DRUPAL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DRUPAL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_DYALOG:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_DYALOG;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_EARLYBIRDS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_EARLYBIRDS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_EBAY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_EBAY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_EDGE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_EDGE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ELEMENTOR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ELEMENTOR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ELLO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ELLO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_EMBER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_EMBER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_EMPIRE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_EMPIRE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ENVIRA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ENVIRA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ERLANG:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ERLANG;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ETHEREUM:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ETHEREUM;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ETSY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ETSY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_EXPEDITEDSSL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_EXPEDITEDSSL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FACEBOOK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FACEBOOK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FACEBOOK_F:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FACEBOOK_F;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FACEBOOK_MESSENGER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FACEBOOK_MESSENGER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FACEBOOK_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FACEBOOK_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FANTASY_FLIGHT_GAMES:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FANTASY_FLIGHT_GAMES;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FIREFOX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FIREFOX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FIRST_ORDER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FIRST_ORDER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FIRST_ORDER_ALT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FIRST_ORDER_ALT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FIRSTDRAFT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FIRSTDRAFT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FLICKR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FLICKR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FLIPBOARD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FLIPBOARD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FLY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FLY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FONT_AWESOME:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FONT_AWESOME;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FONT_AWESOME_ALT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FONT_AWESOME_ALT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FONT_AWESOME_FLAG:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FONT_AWESOME_FLAG;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FONTICONS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FONTICONS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FONTICONS_FI:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FONTICONS_FI;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FORT_AWESOME:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FORT_AWESOME;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FORT_AWESOME_ALT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FORT_AWESOME_ALT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FORUMBEE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FORUMBEE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FOURSQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FOURSQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FREE_CODE_CAMP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FREE_CODE_CAMP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FREEBSD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FREEBSD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_FULCRUM:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_FULCRUM;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GALACTIC_REPUBLIC:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GALACTIC_REPUBLIC;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GALACTIC_SENATE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GALACTIC_SENATE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GET_POCKET:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GET_POCKET;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GG:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GG;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GG_CIRCLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GG_CIRCLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GIT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GIT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GIT_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GIT_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GITHUB:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GITHUB;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GITHUB_ALT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GITHUB_ALT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GITHUB_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GITHUB_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GITKRAKEN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GITKRAKEN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GITLAB:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GITLAB;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GITTER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GITTER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GLIDE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GLIDE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GLIDE_G:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GLIDE_G;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOFORE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOFORE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOODREADS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOODREADS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOODREADS_G:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOODREADS_G;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOOGLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOOGLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOOGLE_DRIVE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOOGLE_DRIVE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOOGLE_PLAY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOOGLE_PLAY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOOGLE_PLUS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOOGLE_PLUS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOOGLE_PLUS_G:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOOGLE_PLUS_G;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOOGLE_PLUS_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOOGLE_PLUS_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GOOGLE_WALLET:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GOOGLE_WALLET;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GRATIPAY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GRATIPAY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GRAV:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GRAV;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GRIPFIRE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GRIPFIRE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GRUNT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GRUNT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_GULP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_GULP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HACKER_NEWS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HACKER_NEWS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HACKER_NEWS_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HACKER_NEWS_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HACKERRANK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HACKERRANK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HIPS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HIPS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HIRE_A_HELPER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HIRE_A_HELPER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HOOLI:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HOOLI;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HORNBILL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HORNBILL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HOTJAR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HOTJAR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HOUZZ:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HOUZZ;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HTML5:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HTML5;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_HUBSPOT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_HUBSPOT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_IMDB:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_IMDB;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_INSTAGRAM:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_INSTAGRAM;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_INTERNET_EXPLORER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_INTERNET_EXPLORER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_IOXHOST:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_IOXHOST;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ITUNES:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ITUNES;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ITUNES_NOTE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ITUNES_NOTE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_JAVA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_JAVA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_JEDI_ORDER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_JEDI_ORDER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_JENKINS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_JENKINS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_JOGET:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_JOGET;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_JOOMLA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_JOOMLA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_JS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_JS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_JS_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_JS_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_JSFIDDLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_JSFIDDLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_KAGGLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_KAGGLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_KEYBASE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_KEYBASE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_KEYCDN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_KEYCDN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_KICKSTARTER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_KICKSTARTER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_KICKSTARTER_K:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_KICKSTARTER_K;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_KORVUE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_KORVUE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LARAVEL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LARAVEL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LASTFM:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LASTFM;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LASTFM_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LASTFM_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LEANPUB:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LEANPUB;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LESS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LESS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LINE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LINE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LINKEDIN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LINKEDIN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LINKEDIN_IN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LINKEDIN_IN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LINODE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LINODE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LINUX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LINUX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_LYFT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_LYFT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MAGENTO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MAGENTO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MAILCHIMP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MAILCHIMP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MANDALORIAN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MANDALORIAN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MARKDOWN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MARKDOWN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MASTODON:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MASTODON;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MAXCDN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MAXCDN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MEDAPPS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MEDAPPS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MEDIUM:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MEDIUM;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MEDIUM_M:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MEDIUM_M;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MEDRT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MEDRT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MEETUP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MEETUP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MEGAPORT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MEGAPORT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MICROSOFT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MICROSOFT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MIX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MIX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MIXCLOUD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MIXCLOUD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MIZUNI:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MIZUNI;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MODX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MODX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_MONERO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_MONERO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NAPSTER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NAPSTER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NEOS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NEOS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NIMBLR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NIMBLR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NINTENDO_SWITCH:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NINTENDO_SWITCH;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NODE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NODE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NODE_JS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NODE_JS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NPM:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NPM;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NS8:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NS8;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_NUTRITIONIX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_NUTRITIONIX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ODNOKLASSNIKI:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ODNOKLASSNIKI;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ODNOKLASSNIKI_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ODNOKLASSNIKI_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_OLD_REPUBLIC:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_OLD_REPUBLIC;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_OPENCART:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_OPENCART;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_OPENID:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_OPENID;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_OPERA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_OPERA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_OPTIN_MONSTER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_OPTIN_MONSTER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_OSI:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_OSI;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PAGE4:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PAGE4;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PAGELINES:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PAGELINES;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PALFED:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PALFED;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PATREON:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PATREON;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PAYPAL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PAYPAL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PENNY_ARCADE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PENNY_ARCADE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PERISCOPE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PERISCOPE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PHABRICATOR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PHABRICATOR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PHOENIX_FRAMEWORK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PHOENIX_FRAMEWORK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PHOENIX_SQUADRON:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PHOENIX_SQUADRON;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PHP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PHP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PIED_PIPER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PIED_PIPER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PIED_PIPER_ALT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PIED_PIPER_ALT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PIED_PIPER_HAT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PIED_PIPER_HAT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PIED_PIPER_PP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PIED_PIPER_PP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PINTEREST:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PINTEREST;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PINTEREST_P:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PINTEREST_P;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PINTEREST_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PINTEREST_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PLAYSTATION:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PLAYSTATION;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PRODUCT_HUNT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PRODUCT_HUNT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PUSHED:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PUSHED;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_PYTHON:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_PYTHON;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_QQ:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_QQ;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_QUINSCAPE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_QUINSCAPE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_QUORA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_QUORA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_R_PROJECT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_R_PROJECT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_RAVELRY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_RAVELRY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_REACT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_REACT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_REACTEUROPE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_REACTEUROPE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_README:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_README;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_REBEL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_REBEL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_RED_RIVER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_RED_RIVER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_REDDIT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_REDDIT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_REDDIT_ALIEN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_REDDIT_ALIEN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_REDDIT_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_REDDIT_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_RENREN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_RENREN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_REPLYD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_REPLYD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_RESEARCHGATE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_RESEARCHGATE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_RESOLVING:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_RESOLVING;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_REV:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_REV;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ROCKETCHAT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ROCKETCHAT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ROCKRMS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ROCKRMS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SAFARI:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SAFARI;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SASS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SASS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SCHLIX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SCHLIX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SCRIBD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SCRIBD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SEARCHENGIN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SEARCHENGIN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SELLCAST:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SELLCAST;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SELLSY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SELLSY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SERVICESTACK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SERVICESTACK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SHIRTSINBULK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SHIRTSINBULK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SHOPWARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SHOPWARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SIMPLYBUILT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SIMPLYBUILT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SISTRIX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SISTRIX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SITH:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SITH;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SKYATLAS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SKYATLAS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SKYPE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SKYPE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SLACK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SLACK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SLACK_HASH:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SLACK_HASH;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SLIDESHARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SLIDESHARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SNAPCHAT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SNAPCHAT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SNAPCHAT_GHOST:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SNAPCHAT_GHOST;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SNAPCHAT_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SNAPCHAT_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SOUNDCLOUD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SOUNDCLOUD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SPEAKAP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SPEAKAP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SPOTIFY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SPOTIFY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SQUARESPACE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SQUARESPACE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STACK_EXCHANGE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STACK_EXCHANGE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STACK_OVERFLOW:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STACK_OVERFLOW;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STAYLINKED:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STAYLINKED;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STEAM:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STEAM;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STEAM_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STEAM_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STEAM_SYMBOL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STEAM_SYMBOL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STICKER_MULE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STICKER_MULE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STRAVA:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STRAVA;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STRIPE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STRIPE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STRIPE_S:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STRIPE_S;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STUDIOVINARI:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STUDIOVINARI;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STUMBLEUPON:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STUMBLEUPON;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_STUMBLEUPON_CIRCLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_STUMBLEUPON_CIRCLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SUPERPOWERS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SUPERPOWERS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_SUPPLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_SUPPLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TEAMSPEAK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TEAMSPEAK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TELEGRAM:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TELEGRAM;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TELEGRAM_PLANE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TELEGRAM_PLANE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TENCENT_WEIBO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TENCENT_WEIBO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_THE_RED_YETI:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_THE_RED_YETI;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_THEMECO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_THEMECO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_THEMEISLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_THEMEISLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_THINK_PEAKS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_THINK_PEAKS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TRADE_FEDERATION:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TRADE_FEDERATION;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TRELLO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TRELLO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TRIPADVISOR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TRIPADVISOR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TUMBLR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TUMBLR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TUMBLR_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TUMBLR_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TWITCH:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TWITCH;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TWITTER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TWITTER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TWITTER_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TWITTER_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_TYPO3:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_TYPO3;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_UBER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_UBER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_UIKIT:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_UIKIT;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_UNIREGISTRY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_UNIREGISTRY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_UNTAPPD:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_UNTAPPD;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_USB:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_USB;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_USSUNNAH:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_USSUNNAH;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VAADIN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VAADIN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VIACOIN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VIACOIN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VIADEO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VIADEO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VIADEO_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VIADEO_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VIBER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VIBER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VIMEO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VIMEO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VIMEO_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VIMEO_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VIMEO_V:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VIMEO_V;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VINE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VINE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VK:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VK;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VNV:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VNV;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_VUEJS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_VUEJS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WEEBLY:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WEEBLY;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WEIBO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WEIBO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WEIXIN:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WEIXIN;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WHATSAPP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WHATSAPP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WHATSAPP_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WHATSAPP_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WHMCS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WHMCS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WIKIPEDIA_W:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WIKIPEDIA_W;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WINDOWS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WINDOWS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WIX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WIX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WIZARDS_OF_THE_COAST:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WIZARDS_OF_THE_COAST;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WOLF_PACK_BATTALION:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WOLF_PACK_BATTALION;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WORDPRESS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WORDPRESS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WORDPRESS_SIMPLE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WORDPRESS_SIMPLE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WPBEGINNER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WPBEGINNER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WPEXPLORER:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WPEXPLORER;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WPFORMS:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WPFORMS;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_WPRESSR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_WPRESSR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_XBOX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_XBOX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_XING:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_XING;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_XING_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_XING_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_Y_COMBINATOR:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_Y_COMBINATOR;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_YAHOO:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_YAHOO;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_YANDEX:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_YANDEX;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_YANDEX_INTERNATIONAL:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_YANDEX_INTERNATIONAL;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_YELP:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_YELP;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_YOAST:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_YOAST;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_YOUTUBE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_YOUTUBE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_YOUTUBE_SQUARE:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_YOUTUBE_SQUARE;
                    break;

                case FontAwesomeIconType.BRAND_REGULAR_ZHIHU:
                    unicode = FontAwesomeUnicode.BRAND_REGULAR_ZHIHU;
                    break;

                default:
                    break;
            }
            return unicode;
        }
    }
}
