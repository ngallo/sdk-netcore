

using Permguard.AzReq;

namespace Permguard.Grpc
{
    public class MapService
    {
        public static Policydecisionpoint.PolicyStore MapPolicyStoreToGrpcPolicyStore(PolicyStore policyStore)
        {
            if (policyStore == null)
            {
                return null;
            }

            return new Policydecisionpoint.PolicyStore
            {
                Id = policyStore.Id,
                Kind = policyStore.Kind
            };
        }
        
        public static Policydecisionpoint.Principal MapPrincipalToGrpcPrincipal(Principal principal)
        {
            if (principal == null)
            {
                return null;
            }

            return new Policydecisionpoint.Principal
            {
                Id = principal.Id,
                Type = principal.Type,
                Source = string.IsNullOrEmpty(principal.Source) ? null : principal.Source
            };
        }
        
        public static Policydecisionpoint.Entities MapEntitiesToGrpcEntities(Entities entities)
        {
            if (entities == null)
            {
                return null;
            }

            var target = new Policydecisionpoint.Entities
            {
                Schema = entities.Schema
            };
            var results = Grpc.Converter.ToRepeatedField(entities.Items);
            foreach(var result in results)
            {
                target.Items.Add(result);
            }
            
            return target;
        }

        public static Policydecisionpoint.Subject MapSubjectToGrpcSubject(Subject subject)
        {
            if (subject == null)
            {
                return null;
            }

            var target = new Policydecisionpoint.Subject
            {
                Id = subject.Id,
                Type = subject.Type,
                Source = string.IsNullOrEmpty(subject.Source) ? null : subject.Source,
                Properties = subject.Properties == null ? null : Grpc.Converter.FromDictionary(subject.Properties)
            };

            return target;
        }
        
        public static Policydecisionpoint.Resource MapResourceToGrpcResource(Resource resource)
        {
            if (resource == null)
            {
                return null;
            }

            return new Policydecisionpoint.Resource
            {
                Id = resource.Id,
                Type = resource.Type,
                Properties = resource.Properties == null ? null : Grpc.Converter.FromDictionary(resource.Properties)
            };
        }
        
        public static Policydecisionpoint.Action MapActionToGrpcAction(Permguard.AzReq.Action action)
        {
            if (action == null)
            {
                return null;
            }

            return new Policydecisionpoint.Action
            {
                Name = action.Name,
                Properties = Grpc.Converter.FromDictionary(action.Properties)
            };
        }
        
        public static Policydecisionpoint.EvaluationRequest MapEvaluationToGrpcEvaluationRequest(Evaluation evaluation)
        {
            if (evaluation == null)
            {
                return null;
            }

            var target = new Policydecisionpoint.EvaluationRequest
            {
                RequestId = evaluation.RequestId,
                Subject = evaluation.Subject == null ? null : MapSubjectToGrpcSubject(evaluation.Subject),
                Resource = evaluation.Resource == null ? null : MapResourceToGrpcResource(evaluation.Resource),
                Action = evaluation.Action == null ? null : MapActionToGrpcAction(evaluation.Action),
                Context = evaluation.Context == null ? null : Grpc.Converter.FromDictionary(evaluation.Context)
            };

            return target;
        }
        
        public static Policydecisionpoint.AuthorizationModelRequest MapAuthZModelToGrpcAuthorizationModelRequest(AZModel azModel)
        {
            var req = new Policydecisionpoint.AuthorizationModelRequest()
            {
                ZoneId = azModel.ZoneId
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
        
        public static Policydecisionpoint.AuthorizationCheckRequest MapAZRequestToGrpcAuthorizationCheckRequest(AZRequest azRequest)
        {
            if (azRequest == null)
            {
                return null;
            }

            var req = new Policydecisionpoint.AuthorizationCheckRequest
            {
                RequestId = azRequest.RequestId
            };

            if (azRequest.AuthorizationModel != null)
            {
                req.AuthorizationModel = MapAuthZModelToGrpcAuthorizationModelRequest(azRequest.AuthorizationModel);
            }

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
                req.Context = Grpc.Converter.FromDictionary(azRequest.Context);
            }

            if (azRequest.Evaluations != null)
            {
                req.Evaluations.Add(azRequest.Evaluations.Select(MapEvaluationToGrpcEvaluationRequest).ToList());
            }

            return req;
        }
        
        public static ReasonResponse MapGrpcReasonResponseToReasonResponse(Policydecisionpoint.ReasonResponse reasonResponse)
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
        
        public static ContextResponse MapGrpcContextResponseToContextResponse(Policydecisionpoint.ContextResponse contextResponse)
        {
            if (contextResponse == null)
            {
                return null;
            }

            var target = new ContextResponse
            {
                Id = contextResponse.Id,
                ReasonAdmin = contextResponse.ReasonAdmin == null ? null : MapGrpcReasonResponseToReasonResponse(contextResponse.ReasonAdmin),
                ReasonUser = contextResponse.ReasonUser == null ? null : MapGrpcReasonResponseToReasonResponse(contextResponse.ReasonUser)
            };

            return target;
        }
        
        public static EvaluationResponse MapGrpcEvaluationResponseToEvaluationResponse(Policydecisionpoint.EvaluationResponse evaluationResponse)
        {
            if (evaluationResponse == null)
            {
                return null;
            }

            var target = new EvaluationResponse
            {
                Decision = evaluationResponse.Decision,
                RequestId = evaluationResponse.RequestId ?? ""
            };

            if (evaluationResponse.Context != null)
            {
                target.Context = MapGrpcContextResponseToContextResponse(evaluationResponse.Context);
            }

            return target;
        }
        
        public static AZResponse MapGrpcAuthorizationCheckResponseToAZResponse(Policydecisionpoint.AuthorizationCheckResponse response)
        {
            if (response == null)
            {
                return null;
            }

            var target = new AZResponse
            {
                Decision = response.Decision,
                RequestId = response.RequestId ?? ""
            };

            if (response.Context != null)
            {
                target.Context = MapGrpcContextResponseToContextResponse(response.Context);
            }

            if (response.Evaluations != null)
            {
                target.Evaluations = response.Evaluations.Select(evaluationResponse => MapGrpcEvaluationResponseToEvaluationResponse(evaluationResponse)).ToList();
            }

            return target;
        }
    }
}
