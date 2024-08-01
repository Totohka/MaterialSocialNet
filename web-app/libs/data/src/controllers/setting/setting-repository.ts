import {
  SettingPrivacyPutBody,
  SettingNotificationPutBody,
} from './setting-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  responseJson,
} from '@socnet/shared';

/** /api/v1/setting */
class SettingRepository {
  constructor(private _httpClient: HttpBackendClient) {}

  /** PUT /api/v1/setting/privacy */
  putPrivacy(params: SettingPrivacyPutBody) {
    return this._httpClient
      .post('/api/v1/setting/privacy', body(params))
      .then(responseJson<string>);
  }

  /** PUT /api/v1/setting/privacy */
  putNotification(params: SettingNotificationPutBody) {
    return this._httpClient
      .post('/api/v1/setting/notification', body(params))
      .then(responseJson<string>);
  }
}

export const getSettingRepository = () =>
  new SettingRepository(getHttpBackendClient());
