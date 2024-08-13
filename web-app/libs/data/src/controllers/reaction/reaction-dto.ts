/** GET /api/v1/reaction{entityType} Query*/
export type ReactionGetQuery = {
  entityId: number;
};

/** POST /api/v1/reaction{entityType} Body*/
export type ReactionPostBody = {
  entityId: number;
  typeReactionId: number;
  userId: number;
};

/** PUT /api/v1/reaction{entityType} Body*/
export type ReactionPutBody = {
  entityId: number;
  typeReactionId: number;
  typeReactionOldId: number;
  userId: number;
};

/** DELETE /api/v1/reaction{entityType} Body*/
export type ReactionDeleteBody = {
  entityId: number;
  typeReactionId: number;
  userId: number;
};

/** GET /api/v1/reaction{entityType}/all Query*/
export type ReactionGetListQuery = {
  entityId: number;
  number: number;
};

/** GET /api/v1/reaction{entityType}/all Return*/
export type ReactionGetListReturn = {
  countAllReactionEntities: number;
  pageCount: number;
  numberPage: number;
  reactionEntitiesDTO: {
    reactionEntityId: number;
    entityId: number;
    typeReactionId: number;
    type: string | null;
    userId: number;
    firstNameUser: string | null;
    lastNameUser: string | null;
  }[];
};
