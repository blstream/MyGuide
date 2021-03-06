//
//  AFXMLParser.h
//  MyGuide
//
//  Created by afilipowicz on 18.02.2014.
//  Copyright (c) 2014 BLStream - Rafał Korżyński. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "AFAnimal.h"
#import "AFWay.h"
#import "AFNode.h"
#import "AFJunction.h"

@interface AFXMLParser : NSObject <NSXMLParserDelegate>

@property (nonatomic, readonly) NSMutableDictionary *animalInfoDictionaryPL;
@property (nonatomic, readonly) NSMutableDictionary *animalInfoDictionaryEN;
@property (nonatomic, readonly) NSMutableArray      *animalsArrayPL;
@property (nonatomic, readonly) NSMutableArray      *animalsArrayEN;
@property (nonatomic, readonly) NSMutableArray      *waysArray;
@property (nonatomic, readonly) NSMutableArray      *junctionsArray;
@property (nonatomic, readonly) NSMutableArray      *nodesArray;
@property (nonatomic, readonly) NSString            *currentElement;
@property (nonatomic, readonly) NSMutableString     *elementValue;

@property (nonatomic, readonly) BOOL parsingError;
@property (nonatomic, readonly) BOOL nameFlag;
@property (nonatomic, readonly) BOOL descriptionAdultFlag;
@property (nonatomic, readonly) BOOL descriptionChildFlag;

@property (nonatomic, readonly) AFAnimal    *currentAnimalPL;
@property (nonatomic, readonly) AFAnimal    *currentAnimalEN;
@property (nonatomic, readonly) AFWay       *currentWay;
@property (nonatomic, readonly) AFNode      *currentNode;
@property (nonatomic, readonly) AFNode      *temporaryNode;
@property (nonatomic, readonly) AFJunction  *currentJunction;

- (NSData *)getDataXML;
- (void)parse;

@end