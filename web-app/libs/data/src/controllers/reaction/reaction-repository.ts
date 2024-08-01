import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  queryParams,
  responseJson,
} from '@socnet/shared';
import {
  ReactionDeleteBody,
  ReactionGetListQuery,
  ReactionGetListReturn,
  ReactionGetQuery,
  ReactionPostBody,
  ReactionPutBody,
} from './reaction-dto';

/** /api/v1/reactionMessage */
class ReactionRepository {
  constructor(
    private _httpClient: HttpBackendClient,
    private _entityUrl: string
  ) {}

  /** GET /api/v1/reaction{entity} */
  get(query: ReactionGetQuery) {
    return this._httpClient
      .get(this._entityUrl, queryParams(query))
      .then(responseJson<number>);
  }

  /** GET /api/v1/reaction{entity}/all */
  getAll(query: ReactionGetListQuery) {
    return this._httpClient
      .get(this._entityUrl, queryParams(query))
      .then(responseJson<ReactionGetListReturn>);
  }

  /** POST /api/v1/reaction{entity} */
  post(payload: ReactionPostBody) {
    return this._httpClient
      .post(this._entityUrl, body(payload))
      .then(responseJson<void>);
  }

  /** PUT /api/v1/reaction{entity} */
  put(payload: ReactionPutBody) {
    return this._httpClient
      .put(this._entityUrl, body(payload))
      .then(responseJson<void>);
  }

  /** DELETE /api/v1/reactionMessage */
  delete(payload: ReactionDeleteBody) {
    return this._httpClient
      .delete(this._entityUrl, body(payload))
      .then(responseJson<void>);
  }
}

@injectable()
export class ReactionPostRepository extends ReactionRepository {
  constructor(@inject(TYPES.HttpBackendClient) httpClient: HttpBackendClient) {
    super(httpClient, '/api/v1/ReactionPost');
  }
}

@injectable()
export class ReactionMessageRepository extends ReactionRepository {
  constructor(@inject(TYPES.HttpBackendClient) httpClient: HttpBackendClient) {
    super(httpClient, '/api/v1/ReactionMessage');
  }
}

export const getReactionPostRepository = () =>
  DIContainer.get<ReactionPostRepository>(TYPES.ReactionPostRepository);

export const getReactionMessageRepository = () =>
  DIContainer.get<ReactionMessageRepository>(TYPES.ReactionMessageRepository);
