

using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Permguard.AzReq;
using Policydecisionpoint;
using Action = Permguard.AzReq.Action;
using ContextResponse = Permguard.AzReq.ContextResponse;
using Entities = Permguard.AzReq.Entities;
using EvaluationResponse = Permguard.AzReq.EvaluationResponse;
using PolicyStore = Permguard.AzReq.PolicyStore;
using Principal = Permguard.AzReq.Principal;
using ReasonResponse = Permguard.AzReq.ReasonResponse;
using Resource = Permguard.AzReq.Resource;
using Subject = Permguard.AzReq.Subject;

namespace Permguard.Grpc
{
    internal static class MapService
    {
        private static Struct FromDictionary(Dictionary<string, object>? dict)
        {
            var structObj = new Struct();

            if (dict == null) return structObj;
            foreach (var (key, value) in dict)
            {
                var convertedValue = ConvertToValue(value);
                structObj.Fields.Add(key, convertedValue);
            }

            return structObj;
        }

        public static RepeatedField<Struct> ToRepeatedField(List<Dictionary<string, object>?>? items)
        {
            var repeatedField = new RepeatedField<Struct>();

            if (items != null)
            {
                foreach (var item in items)
                {
                    repeatedField.Add(FromDictionary(item));
                }
            }

            return repeatedField;
        }

        private static Value ConvertToValue(object value)
        {
            switch (value)
            {
                case string str:
                    return new Value { StringValue = str };
                case bool boolean:
                    return new Value { BoolValue = boolean };
                case double dbl:
                    return new Value { NumberValue = dbl };
                case int integer:
                    return new Value { NumberValue = integer };
                case long l:
                    return new Value { NumberValue = l };
                case DateTime dateTime:
                    return new Value { StringValue = dateTime.ToString("o") }; // Formato ISO 8601
                case Dictionary<string, object> dict:
                    // Ricorsivamente converte i dizionari
                    return new Value { StructValue = ToStruct(dict) };
                default:
                    return new Value(); 
            }
        }

        private static Struct ToStruct(Dictionary<string, object> dictionary)
        {
            var structObj = new Struct();

            foreach (var kvp in dictionary)
            {
                structObj.Fields.Add(kvp.Key, ConvertToValue(kvp.Value));
            }

            return structObj;
        }

        public static Dictionary<string, object?> ToDictionary(Struct structObj)
        {
            var dict = new Dictionary<string, object?>();

            if (structObj.Fields != null)
            {
                foreach (var kvp in structObj.Fields)
                {
                    var value = ConvertFromValue(kvp.Value);
                    dict.Add(kvp.Key, value);
                }
            }

            return dict;
        }

        private static object? ConvertFromValue(Value value)
        {
            if (value.HasNullValue) 
                return null;

            switch (value.KindCase)
            {
                case Value.KindOneofCase.StringValue:
                    return value.StringValue;
                case Value.KindOneofCase.NumberValue:
                    return value.NumberValue;
                case Value.KindOneofCase.BoolValue:
                    return value.BoolValue;
                case Value.KindOneofCase.NullValue:
                    return null;
                default:
                    throw new InvalidOperationException("Unsupported value type.");
            }
        }

            private static Policydecisionpoint.PolicyStore? MapPolicyStoreToGrpcPolicyStore(PolicyStore? policyStore)
            {
                if (policyStore == null)
                {
                    return null;
                }

                return new Policydecisionpoint.PolicyStore
                {
                    ID = policyStore.Id,
                    Kind = policyStore.Kind
                };
            }

            private static Policydecisionpoint.Principal? MapPrincipalToGrpcPrincipal(Principal? principal)
            {
                if (principal == null)
                {
                    return null;
                }

                return new Policydecisionpoint.Principal
                {
                    ID = principal.Id,
                    Type = principal.Type,
                    Source = string.IsNullOrEmpty(principal.Source) ? null : principal.Source
                };
            }

            private static Policydecisionpoint.Entities? MapEntitiesToGrpcEntities(Entities? entities)
            {
                if (entities == null)
                {
                    return null;
                }

                var target = new Policydecisionpoint.Entities
                {
                    Schema = entities.Schema
                };
                var results = ToRepeatedField(entities.Items);
                foreach(var result in results)
                {
                    target.Items.Add(result);
                }
                
                return target;
            }

            private static Policydecisionpoint.Subject? MapSubjectToGrpcSubject(Subject? subject)
            {
                if (subject == null)
                {
                    return null;
                }

                var target = new Policydecisionpoint.Subject
                {
                    ID = subject.Id,
                    Type = subject.Type,
                    Source = string.IsNullOrEmpty(subject.Source) ? null : subject.Source,
                    Properties = subject.Properties == null ? null : FromDictionary(subject.Properties)
                };

                return target;
            }

            private static Policydecisionpoint.Resource? MapResourceToGrpcResource(Resource? resource)
            {
                if (resource == null)
                {
                    return null;
                }

                return new Policydecisionpoint.Resource
                {
                    ID = resource.Id,
                    Type = resource.Type,
                    Properties = resource.Properties == null ? null : FromDictionary(resource.Properties)
                };
            }

            private static Policydecisionpoint.Action? MapActionToGrpcAction(Action? action)
            {
                if (action == null)
                {
                    return null;
                }

                return new Policydecisionpoint.Action
                {
                    Name = action.Name,
                    Properties = FromDictionary(action.Properties)
                };
            }

            private static EvaluationRequest? MapEvaluationToGrpcEvaluationRequest(Evaluation? evaluation)
            {
                if (evaluation == null)
                {
                    return null;
                }

                var target = new EvaluationRequest
                {
                    RequestID = evaluation.RequestId,
                    Subject = evaluation.Subject == null ? null : MapSubjectToGrpcSubject(evaluation.Subject),
                    Resource = evaluation.Resource == null ? null : MapResourceToGrpcResource(evaluation.Resource),
                    Action = evaluation.Action == null ? null : MapActionToGrpcAction(evaluation.Action),
                    Context = evaluation.Context == null ? null : FromDictionary(evaluation.Context)
                };

                return target;
            }

            private static AuthorizationModelRequest MapAuthZModelToGrpcAuthorizationModelRequest(AzModel azModel)
            {
                var req = new AuthorizationModelRequest()
                {
                    ZoneID = azModel.ZoneId
                };

                if (azModel.PolicyStore != null)
                {
                    req.PolicyStore = MapPolicyStoreToGrpcPolicyStore(azModel.PolicyStore);
                }

                if (azModel.Principal != null)
                {
                    req.Principal = MapPrincipalToGrpcPrincipal(azModel.Principal);
                }

                if (azModel.Entities != null)
                {
                    req.Entities = MapEntitiesToGrpcEntities(azModel.Entities);
                }

                return req;
            }
            
            public static AuthorizationCheckRequest? MapAzRequestToGrpcAuthorizationCheckRequest(AzRequest? azRequest)
            {
                if (azRequest == null)
                {
                    return null;
                }

                var req = new AuthorizationCheckRequest
                {
                    RequestID = azRequest.RequestId,
                    AuthorizationModel = MapAuthZModelToGrpcAuthorizationModelRequest(azRequest.AuthorizationModel)
                };

                if (azRequest.Subject != null)
                {
                    req.Subject = MapSubjectToGrpcSubject(azRequest.Subject);
                }

                if (azRequest.Resource != null)
                {
                    req.Resource = MapResourceToGrpcResource(azRequest.Resource);
                }

                if (azRequest.Action != null)
                {
                    req.Action = MapActionToGrpcAction(azRequest.Action);
                }

                if (azRequest.Context != null)
                {
                    req.Context = FromDictionary(azRequest.Context);
                }

                req.Evaluations.Add(azRequest.Evaluations?.Select(MapEvaluationToGrpcEvaluationRequest).ToList());
                return req;
            }

            private static ReasonResponse? MapGrpcReasonResponseToReasonResponse(Policydecisionpoint.ReasonResponse? reasonResponse)
            {
                if (reasonResponse == null)
                {
                    return null;
                }

                return new ReasonResponse
                {
                    Code = reasonResponse.Code,
                    Message = reasonResponse.Message
                };
            }

            private static ContextResponse? MapGrpcContextResponseToContextResponse(Policydecisionpoint.ContextResponse? contextResponse)
            {
                if (contextResponse == null)
                {
                    return null;
                }

                var target = new ContextResponse
                {
                    Id = contextResponse.ID,
                    ReasonAdmin = contextResponse.ReasonAdmin == null ? null : MapGrpcReasonResponseToReasonResponse(contextResponse.ReasonAdmin),
                    ReasonUser = contextResponse.ReasonUser == null ? null : MapGrpcReasonResponseToReasonResponse(contextResponse.ReasonUser)
                };

                return target;
            }

            private static EvaluationResponse? MapGrpcEvaluationResponseToEvaluationResponse(Policydecisionpoint.EvaluationResponse? evaluationResponse)
            {
                if (evaluationResponse == null)
                {
                    return null;
                }

                var target = new EvaluationResponse
                {
                    Decision = evaluationResponse.Decision,
                    RequestId = evaluationResponse.RequestID ?? ""
                };

                if (evaluationResponse.Context != null)
                {
                    target.Context = MapGrpcContextResponseToContextResponse(evaluationResponse.Context);
                }

                return target;
            }
            
            public static AzResponse? MapGrpcAuthorizationCheckResponseToAzResponse(AuthorizationCheckResponse? response)
            {
                if (response == null)
                {
                    return null;
                }

                var target = new AzResponse
                {
                    Decision = response.Decision,
                    RequestId = response.RequestID ?? ""
                };

                if (response.Context != null)
                {
                    target.Context = MapGrpcContextResponseToContextResponse(response.Context);
                }

                if (response.Evaluations != null)
                {
                    target.Evaluations = response.Evaluations?.Select(MapGrpcEvaluationResponseToEvaluationResponse).ToList()!;
                }

                return target;
            }
    }
}
