open System

// one liner types

// some "record" types
type Person = {FirstName:string; LastName:string; Dob:DateTime}
type Coord = {Lat:float; Long:float}

// some "union" (choice) types
type TimePeriod = Hour | Day | Week | Year
type Temperature = C of int | F of int
type Appointment = OneTime of DateTime 
                   | Recurring of DateTime list

// DDD types example
type PersonalName = {FirstName:string; LastName:string}

// Addresses
type StreetAddress = {Line1:string; Line2:string; Line3:string }

type ZipCode =  ZipCode of string   
type StateAbbrev =  StateAbbrev of string
type ZipAndState =  {State:StateAbbrev; Zip:ZipCode }
type USAddress = {Street:StreetAddress; Region:ZipAndState}

type UKPostCode =  PostCode of string
type UKAddress = {Street:StreetAddress; Region:UKPostCode}

type InternationalAddress = {
    Street:StreetAddress; Region:string; CountryName:string}

// choice type -- must be one of these three specific types
type Address = UsAddress of USAddress | UkAddress of UKAddress | InternationalAddress of InternationalAddress

// Email
type Email = Email of string

// Phone
type CountryPrefix = Prefix of int
type Phone = {CountryPrefix:CountryPrefix; LocalNumber:string}

type Contact = 
    {
    PersonalName: PersonalName;
    // "option" means it might be missing
    Address: Address option;
    Email: Email option;
    Phone: Phone option;
    }

// Put it all together into a CustomerAccount type
type CustomerAccountId  = AccountId of string
type CustomerType  = Prospect | Active | Inactive

// override equality and deny comparison
[<CustomEquality; NoComparison>]
type CustomerAccount = 
    {
    CustomerAccountId: CustomerAccountId;
    CustomerType: CustomerType;
    ContactInfo: Contact;
    }

    override this.Equals(other) =
        match other with
        | :? CustomerAccount as otherCust -> 
          (this.CustomerAccountId = otherCust.CustomerAccountId)
        | _ -> false

    override this.GetHashCode() = hash this.CustomerAccountId 

// creating Customer account objects
let rafi = {FirstName="Rashed";LastName="Hassan"}
let idOfRafi = AccountId "1"

let streetAdressOfRafi = {Line1="House-594/a";Line2="Yeasha evenue"; Line3="Bloack I"}

let ukAddressOfRafi = {Street=streetAdressOfRafi;Region=PostCode "1800"}
let addressOfRafi = Address.UkAddress ukAddressOfRafi

let contactOfRafi = {
    PersonalName = rafi;
    Address = Some(addressOfRafi);
    Email = Some(Email "kh.rashed900@gmail.com")
    Phone = Some({CountryPrefix=CountryPrefix.Prefix 88; LocalNumber = "01727149177"})
}


let customer1 = {
    CustomerAccountId=idOfRafi;
    CustomerType=Prospect;
    ContactInfo=contactOfRafi
    }

printf "%A" customer1